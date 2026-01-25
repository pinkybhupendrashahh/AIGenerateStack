const BASE_URL = "https://localhost:5001/api/video";
import axios from "axios";
export const videoApi = {
  generate: (payload) =>  axios.post(`${BASE_URL}/generate`, payload),
  status: (jobId) => axios.get(`${BASE_URL}/status/${jobId}`),
  retry: (jobId) => axios.post(`${BASE_URL}/${jobId}/retry`),
  cancel: (jobId) => axios.post(`${BASE_URL}/${jobId}/cancel`)
};
