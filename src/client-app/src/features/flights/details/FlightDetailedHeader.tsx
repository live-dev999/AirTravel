import { observer } from 'mobx-react-lite';
import React from 'react'
import { Button, Header, Item, Segment, Image } from 'semantic-ui-react'
import { Flight } from "../../../app/models/flight";

const flightImageStyle = {
    filter: 'brightness(30%)'
};

const flightImageTextStyle = {
    position: 'absolute',
    bottom: '5%',
    left: '5%',
    width: '100%',
    height: 'auto',
    color: 'white'
};

interface Props {
    flight: Flight
}

export default observer(function FlightDetailedHeader({ flight }: Props) {
    return (
        <Segment.Group>
            <Segment basic attached='top' style={{ padding: '0' }}>
                <Image src={`/assets/categoryImages/${flight.category}.jpg`} fluid style={flightImageStyle} />
                <Segment style={flightImageTextStyle} basic>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Header
                                    size='huge'
                                    content={flight.title}
                                    style={{ color: 'white' }}
                                />
                                <p>{flight.departureTime}</p>
                                <p>
                                    Hosted by <strong>Bob</strong>
                                </p>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
            </Segment>
            <Segment clearing attached='bottom'>
                <Button color='teal'>Join Flight</Button>
                <Button>Cancel attendance</Button>
                <Button color='orange' floated='right'>
                    Manage Event
                </Button>
            </Segment>
        </Segment.Group>
    )
})