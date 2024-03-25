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

import { observer } from "mobx-react-lite";
import { Grid } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import FlightList from "./FlightList";
import { useEffect } from "react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import FlightFilters from "./FlightFilters";

export default observer(function FlightDashboard() {
    const { flightStore } = useStore();
    const { loadActivities: loadFlights, flightRegistry } = flightStore;
    useEffect(() => {
        if (flightRegistry.size <= 1) loadFlights();
    }, [loadFlights, flightRegistry])

    if (flightStore.loadingInitial)
        return (<LoadingComponent content='Loading app...' />)

    return (
        <Grid>
            <Grid.Column width='10'>
                <FlightList />
            </Grid.Column>
            <Grid.Column width='6'>
               {/* <FlightFilters/> */}
            </Grid.Column>

        </Grid>
    )
})