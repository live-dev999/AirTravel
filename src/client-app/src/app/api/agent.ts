/*
 *   Copyright (c) 2024 Dzianis Prokharchyk

 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.

 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.

 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

import axios, { AxiosError, AxiosResponse } from 'axios';
import { Flight } from '../models/flight';
import { toast } from 'react-toastify';
import { router } from '../router/Routes';
import { store } from '../stores/store';
import { PaginatedResult } from '../models/pagination';

// const sleep = (delay: number) => {
//     return new Promise((resolve) => {
//         setTimeout(resolve, delay)
//     })
// }
axios.defaults.baseURL = "http://localhost:5250/api"
axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';
axios.interceptors.response.use(async respose => {

    // await sleep(1000);
    const pagination = respose.headers['pagination']
    if(pagination)
    {
        respose.data = new PaginatedResult(respose.data,JSON.parse(pagination))
        return respose as AxiosResponse<PaginatedResult<unknown>>
    }
    return respose;
}, (error: AxiosError) => {
    const { data, status, config} = error.response as AxiosResponse;
    switch (status) {
        case 400:
            if(config.method === 'get' && Object.prototype.hasOwnProperty.call(data.errors, 'id'))
            {
                 router.navigate('/not-found');
            }
            //toast.error('bad request')
            if (data.errors) {
                const modalStateErrors = [];
                for (const key in data.erros) {
                    if (data.errors[key])
                        modalStateErrors.push(data.errors[key])
                }
                throw modalStateErrors.flat();
            } else {
                toast.error(data);
            }
            break;
        case 401:
            toast.error('unauthorised')
            break;
        case 403:
            toast.error('forbidden')
            break;
        case 404:
            //toast.error('not found')
            router.navigate('/not-found');
            break;
        case 500:
            store.commonStore.setServerError(data);
            router.navigate('/server-error');
            //toast.error('server error ')
            break;
    }
    return Promise.reject(error);
})

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url,).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}

const Activities = {
    list: () => requests.get<PaginatedResult<Flight[]>>('/flights'),
    details: (id: string) => requests.get<Flight>(`/flights/${id}`),
    create: (flight: Flight) => requests.post<void>('/flights', flight),
    update: (flight: Flight) => requests.put<void>(`/flights/${flight.id}`, flight),
    delete: (id: string) => requests.del(`/flights/${id}`),
}

const agent = {
    Activities
}

export default agent;