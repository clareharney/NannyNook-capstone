import React, { useEffect, useState } from 'react';
import { Outlet, useNavigate } from 'react-router-dom';
import { getOccasionByNotUserId } from '../managers/occasionManager.js';
import { Button, Card, CardBody, CardSubtitle, CardText, CardTitle } from 'reactstrap';

const HomePage = ({loggedInUser}) => {
const [occasions, setOccasions] = useState([]);
const navigate = useNavigate();

useEffect(() => {
    getOccasionByNotUserId(loggedInUser.id).then((arr) => setOccasions(arr));
  }, []);


  return (
    <>
    <div className="container">
                <h1>Events List</h1>
                {occasions.map((o) => (
                    
                    <Card
                        key={o.id}
                        style={{
                            width: "10rem",
                        }}
                    >
                        <CardBody>
                            <CardTitle tag="h5">{o.title}</CardTitle>
                            <CardSubtitle className="mb-2 text-muted" tag="h6">
                                {o.hostUserProfile.fullName}
                            </CardSubtitle>
                            <CardText>{o.category?.name}</CardText>
                            <CardText>{o.location}</CardText>
                            <CardText>{o.formattedDate}</CardText>
                            <CardBody>{o.description}</CardBody>
                            <Button
                                onClick={() => {
                                    navigate(`/events/${o.id}`);
                                }}
                            >
                                View Occasion
                            </Button>
                        </CardBody>
                    </Card>
                ))}
            </div>
    </>
  );
};

export default HomePage;
