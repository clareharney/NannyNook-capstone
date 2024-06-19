const _apiUrl = "/api/RSVP";

export const NewRSVP = (RSVP) => {
  return fetch(_apiUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(RSVP),
  }).then((res) => res.json());
};
export const getRSVPs = () => {
    return fetch(_apiUrl).then((res) => res.json());
  };
  
  export const getUserRSVPs = (id) => {
    return fetch(_apiUrl + `/${id}`).then((res) => res.json());
  };
  
  export const UnRSVP = (RSVP) => {
      return fetch(_apiUrl + `${_apiUrl}/${RSVP.UserProfileId}/${RSVP.OccasionId}`, {
        method: "DELETE",
      }).then((response) => {
        if (!response.ok) {
          throw new Error("Failed to un-RSVP");
        }
      });
    };
  