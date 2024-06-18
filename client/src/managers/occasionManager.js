const _apiUrl = "/api/Occasion";

export const getOccasions  = async () => {
  return await fetch(_apiUrl).then((res) => res.json());
};


export const getOccasionById = (id) => {
  return fetch(_apiUrl + `/${id}`).then((res) => res.json());
};

export const createOccasion = (occasion) => {
    return fetch(_apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(occasion)
    }).then((res) => res.json())
};

export const deleteOccasion = (occasionId) => {
  return fetch(_apiUrl + `/${occasionId}`, {
    method: "DELETE",
  }).then((response) => {
    if (!response.ok) {
      throw new Error("Failed to delete occasion");
    }
  });
};

export const editOccasion = (occasion, id) => {
  return fetch(`${_apiUrl}/${id}`, {
    method: "PUT",
    headers: {
        "Content-Type": "application/json",
    },
    body: JSON.stringify(occasion)
  }).then((res) => res.json());
};