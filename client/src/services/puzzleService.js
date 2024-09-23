import AxiosClient from "./axiosClient";

const path = `${import.meta.env.VITE_APP_PUZZLES_PATH}/puzzle`;
const axiosClient = new AxiosClient(path);

const puzzle_service_get_knight_moves_results = () => {
    return axiosClient.get(`/GetKnightMovesResults`);
};

const puzzle_service_delete_knight_moves_result = (id) => {
    return axiosClient.delete(`/DeleteKnightMovesResult?id=${id}`);
};

export {
    puzzle_service_get_knight_moves_results as puzzleServiceGetKnightMovesResults,
    puzzle_service_delete_knight_moves_result as puzzleServiceDeleteKnightMovesResult,
};
