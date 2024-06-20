import { useState, useEffect } from "react"
import { Button, Card, CardBody, CardSubtitle, CardText, CardTitle } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { getJobByNotUserId, getJobs } from "../../managers/jobManager.js";
import "./AllJobs.css"

const MyJobsList = ({ loggedInUser}) => {
    const [jobs, setJobs] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchData = async () => {
          try {
            const jobsData = await getJobs()
            const userJobs = jobsData
              .filter((job) => job.posterId === loggedInUser.id)
            setJobs(userJobs);
          } catch (error) {
            console.error("Error fetching:", error);
          }
        };
    
        fetchData();
      }, [loggedInUser]);


      return (
        <div className="container">
            <h1>My Jobs</h1>
            {jobs.length > 0 ? (
                jobs.map((j) => (
                    <Card
                        key={j.id}
                        style={{
                            width: "10rem",
                        }}
                    >
                        <CardBody>
                            <CardTitle tag="h5">{j.title}</CardTitle>
                            <CardSubtitle className="mb-2 text-muted" tag="h6">
                                {j.poster?.fullName}
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
                ))
            ) : (
                <div>
                    <p className="notif">You haven't created a job posting yet!</p>
                    <Button onClick={() => navigate("/jobs/create")}>
                        Create a Job Posting
                    </Button>
                </div>
            )}
        </div>
    );
}

export default MyJobsList
    