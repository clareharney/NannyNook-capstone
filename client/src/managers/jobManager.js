const _apiUrl = "/api/Job";

export const getJobs  = async () => {
    return await fetch(_apiUrl).then((res) => res.json());
};
  
export const getJobById = async (id) => {
    return await fetch(_apiUrl + `/${id}`).then((res) => res.json());
};

export const getJobByNotUserId = (id) => {
    return fetch(_apiUrl + `/${id}/user`).then((res) => res.json());
  };

// create a PUT
export const editJob = (job, id) => {
    return fetch(`${_apiUrl}/${id}`, {
      method: "PUT",
      headers: {
          "Content-Type": "application/json",
      },
      body: JSON.stringify(job)
    }).then((res) => res.json());
  };

// create a POST
export const createJob = (job) => {
    return fetch(_apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(job)
    }).then((res) => res.json())
};

//create a DELETE
export const deleteJob = (jobId) => {
    return fetch(_apiUrl + `/${jobId}`, {
      method: "DELETE",
    }).then((response) => {
      if (!response.ok) {
        throw new Error("Failed to delete this job posting");
      }
    });
  };
