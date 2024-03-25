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

import { makeAutoObservable, runInAction } from "mobx"
import agent from "../api/agent";
import { Flight } from "../models/flight"
import { v4 as uuid } from 'uuid';
import { Pagination } from "../models/pagination";


export default class FlightStore {
    flightRegistry = new Map<string, Flight>();
    selectedFlight: Flight | undefined = undefined;
    editMode = false;
    loading = false;
    loadingInitial = true;
    pagination: Pagination | null = null;

    constructor() {
        makeAutoObservable(this)
    }
    get flightsByDate() {
        return Array.from(this.flightRegistry.values()).sort((a, b) =>
            Date.parse(a.departureTime) - Date.parse(b.departureTime));
    }

    get groupedActivities() {
        return Object.entries(
            this.flightsByDate.reduce((flights, flight) => {
                const date = flight.departureTime;
                flights[date] = flights[date] ? [...flights[date], flight] : [flight];
                return flights;
            }, {} as { [key: string]: Flight[] })
        )
    }

    loadActivities = async () => {
        this.setLoadingInitial(true);
        try {
            const result = await agent.Activities.list();

            result.data.forEach(flight => {
                this.setFlight(flight);
            });
            this.setPagination(result.pagination);
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

    setPagination = (pagination: Pagination) => {
        this.pagination = pagination;
    }

    loadFlight = async (id: string) => {
        let flight = this.getFlight(id)
        if (flight) this.selectedFlight = flight;
        if (flight) {
            this.selectedFlight = flight;
            return flight;
        }
        else {
            this.setLoadingInitial(true);
            try {
                flight = await agent.Activities.details(id);
                this.setFlight(flight);
                runInAction(() => {
                    this.selectedFlight = flight;
                });
                this.setLoadingInitial(false)
                return flight;
            } catch (error) {
                console.log(error);
                this.setLoadingInitial(false);
            }
        }
    }
    private setFlight = (flight: Flight) => {
        flight.departureTime = flight.departureTime.split('T')[0];
        this.flightRegistry.set(flight.id, flight);
    }
    private getFlight = (id: string) => {
        return this.flightRegistry.get(id);
    }
    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    createFlight = async (flight: Flight) => {
        this.loading = true;
        flight.id = uuid();

        try {
            await agent.Activities.create(flight)
            runInAction(() => {
                this.flightRegistry.set(flight.id, flight);//this.flights.push(flight);
                this.selectedFlight = flight;
                this.editMode = false;
                this.loading = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }
    }

    updateFlight = async (flight: Flight) => {
        this.loading = true;

        try {
            await agent.Activities.update(flight)
            runInAction(() => {
                this.flightRegistry.set(flight.id, flight);//this.flights = [...this.flights.filter(a => a.id !== flight.id), flight];
                this.selectedFlight = flight;
                this.editMode = false;
                this.loading = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }
    }


    deleteFlight = async (id: string) => {
        this.loading = true;

        try {
            await agent.Activities.delete(id)
            runInAction(() => {
                this.flightRegistry.delete(id);//this.flights = [...this.flights.filter(a => a.id !== id)];
                this.selectedFlight = undefined;
                this.editMode = false;
                this.loading = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }
    }
}