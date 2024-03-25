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

import { Link } from "react-router-dom";
import { Item, Button, SegmentGroup, Segment, Icon } from "semantic-ui-react";
import { Flight } from "../../../app/models/flight";

interface Props {
    flight: Flight
}
export default function FlightListItem({ flight }: Props) {
    return (
        <SegmentGroup>
            <Segment>
                <Item.Group>
                    <Item.Content>
                        <Item.Header as={Link} to={`/flights/${flight.id}`}>
                            Ticket
                            {/* {flight.title} */}
                        </Item.Header>
                        {/* <Item.Description> Hosted by Bob
                        </Item.Description> */}

                    </Item.Content>
                    <Item.Image size='tiny' circular src='/assets/fly.jpg' />

                </Item.Group>
            </Segment>
            <Segment>
                <span>
                    <Icon name="clock" /> {flight.departureTime}
                    <br />
                    <Icon name="marker" /> Departure Airport: {flight.from}
                    <br />
                    <Icon name="marker" /> Arrival Airport: {flight.to}
                </span>
            </Segment>
            <Segment secondary>
                Ticket Number: {flight.flightNumber}
            </Segment>
            <Segment clearing>
                <span>
                    {flight.status}
                </span>
                <Button as={Link} to={`/flights/${flight.id}`}
                    color='teal'
                    floated='right'
                    content='View' />
            </Segment>
        </SegmentGroup>
        // <Item key={flight.id}>
        //     <Item.Content>
        //         <Item.Header as='a'>{flight.title}</Item.Header>
        //         <ItemMeta>
        //             {flight.date}
        //         </ItemMeta>
        //         <Item.Description>
        //             <div>{flight.description}</div>
        //             <div>{flight.city}, {flight.venue}</div>
        //         </Item.Description>
        //         <Item.Extra>
        //             <Button as={Link} to={`/flights/${flight.id}`} floated='right' content='View' color='blue' />
        //             {/* <Button onClick={() => flightStore.selectFlight(flight.id)} floated='right' content='View' color='blue' /> */}
        //             <Button
        //                 name={flight.id}
        //                 loading={loading && target === flight.id}
        //                 onClick={(e) => handleFlightDelete(e, flight.id)}
        //                 floated='right' content='Delete' color='red' />
        //             <Label basic content={flight.category} />
        //         </Item.Extra>
        //     </Item.Content>
        // </Item>
    )
}