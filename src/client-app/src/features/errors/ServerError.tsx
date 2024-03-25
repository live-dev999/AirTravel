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
import { useStore } from "../../app/stores/store";
import { Container, Header, Segment } from "semantic-ui-react";

export default observer(function ServerError() {
    const { commonStore } = useStore();

    return (
        <Container ntainer>
            <Header as='h1' content='Server error' />
            <Header subheader='h5' color='red' content={commonStore.error?.message} />
            {commonStore.error?.details && (
                <Segment>
                    <Header as='h4' content='Stack trace' color='teal' />
                    <code style={{ marginTop: '10px' }}> {commonStore.error.details}</code>
                </Segment>
            )}
        </Container>
    )
})