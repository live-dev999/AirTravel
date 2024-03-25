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

import { Navigate, RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import FlightDashboard from "../../features/flights/dashboard/FlightDashboard";
import FlightForm from "../../features/flights/form/FlightForm";
import FlightDetails from "../../features/flights/details/FlightDetails";
import TestErrors from "../../features/errors/TestErrors";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";

export const routes: RouteObject[] =
    [{
        path: '/',
        element: <App />,
        children: [
            {
                path: '/flights',
                element: <FlightDashboard />
            },
            {
                path: '/flights/:id',
                element: <FlightDetails />
            },
            {
                path: 'createFlight',
                element: <FlightForm key='create' />
            },
            {
                path: 'manage/:id',
                element: <FlightForm key='manage' />
            },
            {
                path: 'errors',
                element: <TestErrors />
            },
            {
                path: 'not-found',
                element: <NotFound />
            },
            {
                path: 'server-error',
                element: <ServerError />
            },
            {
                path: '*',
                element: <Navigate replace to='/not-found' />
            }
        ]
    }]
export const router = createBrowserRouter(routes)