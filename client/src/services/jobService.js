import AxiosClient from "./axiosClient";

const path = `${import.meta.env.VITE_APP_PUZZLES_PATH}/job`;
const axiosClient = new AxiosClient(path);

const job_service_put = (job, matrixId) => {
    return axiosClient.put(`/Put?matrixId=${matrixId}`, job);
};

export { job_service_put as jobServicePut };
