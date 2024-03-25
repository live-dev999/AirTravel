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

import { Button, Container, Menu } from "semantic-ui-react";
import { NavLink } from "react-router-dom";

export default function NavBar() {
    return (
        <Menu inverted fixed="top">
            <Container>
                <Menu.Item as={NavLink} to='/' header>
                    <img src="./assets/logo.png" alt="logo" style={{ marginRight: '10px' }} />
                    Air Travel
                </Menu.Item>
                <Menu.Item as={NavLink} to='/flights' name="Tickets"></Menu.Item>
                <Menu.Item as={NavLink} to='/errors' name="Errors">
                </Menu.Item>
                {/* <Menu.Item name="Activities">
                    <Button positive content='Create Flight' as={NavLink} to='/createFlight'/>
                </Menu.Item> */}
            </Container>
        </Menu>
    )
}