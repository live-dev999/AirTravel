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

import { Grid } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";
import { useEffect } from "react";
import FlightDetailedHeader from "./FlightDetailedHeader";
import FlightDetailedInfo from "./FlightDetailedInfo";
import FlightDetailedChat from "./FlightDetailedChat";
import FlightDetailedSidebar from "./FlightDetailedSidebar";


export default observer(function FlightDetails() {
    const { flightStore } = useStore();
    const { selectedFlight: flight, loadFlight, loadingInitial } = flightStore;
    const { id } = useParams();

    useEffect(() => {
        if (id) loadFlight(id);
    }, [id, loadFlight])

    if (loadingInitial || !flight) return <LoadingComponent />;

    return (
        <Grid>
            <Grid.Column width={10}>
                <FlightDetailedHeader flight={flight} />
                <FlightDetailedInfo flight={flight} />
                <FlightDetailedChat />
            </Grid.Column>
            <Grid.Column width={6}>
                <FlightDetailedSidebar />
            </Grid.Column>
        </Grid>
        // <Card fluid>
        //     <Image src={`/assets/categoryImages/${flight.category}.jpg`} />
        //     <Card.Content>
        //         <Card.Header>{flight.title}</Card.Header>
        //         <Card.Meta>
        //             <span>{flight.date}</span>
        //         </Card.Meta>
        //         <Card.Description>
        //             {flight.description}
        //         </Card.Description>
        //     </Card.Content>
        //     <Card.Content extra>
        //         <ButtonGroup widths='2'>
        //             <Button as={Link} to={`/manage/${id}`} basic color='blue' content='Edit' />
        //             <Button as={Link} to={'/flights'} basic color='green' content='Cancel' />
        //         </ButtonGroup>
        //     </Card.Content>
        // </Card >
    )
})