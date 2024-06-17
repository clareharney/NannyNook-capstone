const _apiUrl = "/api/userprofile";

export const getProfiles = () => {
  return fetch(_apiUrl + "/withroles").then((res) => res.json());
};

export const getProfile = async (id) => {
  return await fetch(_apiUrl + `/${id}`).then((res) => res.json());
};

export const getProfileWithRoles = (id) => {
  return fetch(_apiUrl + `/${id}/withroles`).then((res) => res.json());
};

export const promoteUser = (userId, id) => {
  return fetch(`${_apiUrl}/promote/${userId}?profileId=${id}`, {
    method: "POST",
  });
};

export const demoteUser = (userId, id) => {
  return fetch(`${_apiUrl}/demote/${userId}?adminId=${id}`, {
    method: "POST",
  });
};

export const toggleUserActiveStatus = (userId) => {
  return fetch(`${_apiUrl}/${userId}/toggle`, {
    method: "PUT",
  });
};

export const requestUser = (userId, adminId) => {
  return fetch(`${_apiUrl}/${userId}/request?adminId=${adminId}`, {
    method: "PUT",
  });
};

export const denyUser = (userId) => {
  return fetch(`${_apiUrl}/${userId}/deny`, {
    method: "PUT",
  });
};

export const editProfile = (formData, id) => {
  return fetch(`${_apiUrl}/${id}`, {
    method: "PUT",
    body: formData,
  }).then((res) => res.json());
};
