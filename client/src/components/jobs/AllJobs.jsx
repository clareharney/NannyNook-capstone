import React, { useEffect, useState } from 'react';
import { Outlet, useNavigate } from 'react-router-dom';
import { Button, Card, CardBody, CardSubtitle, CardText, CardTitle } from 'reactstrap';
import { getJobByNotUserId } from '../../managers/jobManager.js';

const AllJobs = ({loggedInUser}) => {
const [jobs, setJobs] = useState([]);
const navigate = useNavigate();

useEffect(() => {
    getJobByNotUserId(loggedInUser.id).then((arr) => setJobs(arr));
  }, []);


  return (
    <>
    <div className="container">
                <h1>Events List</h1>
                {jobs.map((j) => (
                    
                    <Card
                        key={j.id}
                        style={{
                            width: "10rem",
                        }}
                    >
                        <CardBody>
                            <CardTitle tag="h5">{j.title}</CardTitle>
                            <CardSubtitle className="mb-2 text-muted" tag="h6">
                                {j.poster.fullName}
                            </CardSubtitle>
                            <CardText>Number of Children : {j.numberOfKids}</CardText>
                            <CardText>{`$${j.payRateMin}-$${j.payRateMax} an hour`}</CardText>
                            <Button
                                onClick={() => {
                                    navigate(`/jobs/${j.id}`);
                                }}
                            >
                                View Job
                            </Button>
                        </CardBody>
                    </Card>
                ))}
            </div>
    </>
  );
};

export default AllJobs;
