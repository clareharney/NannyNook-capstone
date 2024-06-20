const _apiUrl = "/api/Resource";

export const getResources  = async () => {
    return await fetch(_apiUrl).then((res) => res.json());
  };

export const getResourceById = async () => {
    return await fetch(_apiUrl + `/${id}`).then((res) => res.json());
};

