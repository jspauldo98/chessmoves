import AxiosClient from "./axiosClient";

const path = `${import.meta.env.VITE_APP_PUZZLES_PATH}/matrix`;
const axiosClient = new AxiosClient(path);

const matrix_service_get_by_name = (name) => {
    return axiosClient.get(`/GetByName?name=${name}`);
};

const matrix_service_get_all = () => {
    return axiosClient.get(`/GetAll`);
};

const matrix_service_put = (matrix) => {
    return axiosClient.put(`/Put`, matrix);
};

const matrix_service_delete = (matrixId) => {
    return axiosClient.delete(`/Delete?matrixId=${matrixId}`);
};

export {
    matrix_service_get_by_name as matrixServiceGetByName,
    matrix_service_get_all as matrixServiceGetAll,
    matrix_service_put as matrixServicePut,
    matrix_service_delete as matrixServiceDelete,
};
