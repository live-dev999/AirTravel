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
import { useEffect, useState } from "react";
import { Button, Form, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { Link, useNavigate, useParams } from "react-router-dom";
import { Flight } from "../../../app/models/flight";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Formik } from "formik";


export default observer(function FlightForm() {
    const { flightStore } = useStore();
    const {loading, loadFlight, loadingInitial } = flightStore;
    const { id } = useParams();
    const [flight, setFlight] = useState<Flight>({
        id: '',
        title: '',
        category: '',
        description: '',
        departureTime: '',
        from: '',
        to: '',
        flightNumber: '',
        status: ''
    });
    useNavigate();
    useEffect(() => {
        if (id) loadFlight(id).then(flight => setFlight(flight!));
    }, [id, loadFlight])

    if (loadingInitial) return <LoadingComponent content="Loading flight ..." />
    return (
        <Segment clearing>
            <Formik initialValues={flight} onSubmit={values => console.log(values)}>
                {({ values: flight, handleChange, handleSubmit }) =>
                    <Form onSubmit={handleSubmit} autoComplete='off'>
                        {/* <Form.Input placeholder='Title' value={flight.title} name='title' onChange={handleChange} />
                        <Form.TextArea placeholder='Description' value={flight.description} name='description' onChange={handleChange} />
                        <Form.Input placeholder='Category' value={flight.category} name='category' onChange={handleChange} />
                        <Form.Input type="date" placeholder='Date' value={flight.departureTime} name='date' onChange={handleChange} />
                        <Form.Input placeholder='City' value={flight.from} name='city' onChange={handleChange} />
                        <Form.Input placeholder='Venue' value={flight.to} name='venue' onChange={handleChange} /> */}
                        <Button loading={loading} floated='right' positive type='submit' content='Submit' />
                        <Button floated='right' as={Link} to={'/flights'} positive type='button' content='Cancel' />
                    </Form>
                }
            </Formik>

        </Segment>
    )
})