import React, { useEffect, useState } from 'react';
import { Button, Card, CardBody, CardSubtitle, CardText, CardTitle } from 'reactstrap';
import { useNavigate } from 'react-router-dom';
import { getJobByNotUserId } from '../../managers/jobManager.js';
import './AllJobs.css';

const AllJobs = ({ loggedInUser }) => {
  const [jobs, setJobs] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getJobByNotUserId(loggedInUser.id).then((arr) => setJobs(arr));
  }, [loggedInUser.id]);

  return (
    <div className="container">
      <h1>All Jobs</h1>
      <div className="grid-container">
        {jobs.map((j) => (
          <div className="grid-item-jobs" key={j.id}>
            <Card>
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
          </div>
        ))}
      </div>
    </div>
  );
};

export default AllJobs;

