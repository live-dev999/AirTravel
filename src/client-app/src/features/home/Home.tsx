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
import { Button, Container, Header, Image, Segment } from "semantic-ui-react";

export default function HomePage() {
    return (

        <Segment inverted vertical textAlign='center' className="masthead">
            <Container text>
                <Header as='h1' inverted>
                    <Image size='medium' src='/assets/logo.png' alt='logo' style={{ marginButtom: 12 }} />
                </Header>
                <Header as='h2' inverted content='Welcom to AirTravel' />
                <Button as={Link} to='/flights' size='huge' inverted>
                    Go to AirTravel!
                </Button>
            </Container>
        </Segment>
    )
}