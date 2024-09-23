import { useEffect, useState } from "react";
import { matrixServiceGetByName, matrixServicePut } from "@services/matrixService";
import { Matrix } from "@components/Matrix";
import { MatrixLoadSave } from "@components/MatrixLoadSave";
import "./knightMoves.css";
import { matrixServiceDelete, matrixServiceGetAll } from "../services/matrixService";
import { puzzleServiceGetKnightMovesResults } from "@services/puzzleService";
import { KnightsMoveTable } from "@components/KnightMovesTable";
import { jobServicePut } from "../services/jobService";
import { FiRefreshCcw } from "react-icons/fi";
import { puzzleServiceDeleteKnightMovesResult } from "../services/puzzleService";

export const KnightMoves = () => {
    const [matrixData, setMatrixData] = useState([]);
    const [matrixSavedData, setMatrixSavedData] = useState([]);
    const [solveResults, setSolvedResults] = useState([]);
    const [matrixId, setMatrixId] = useState(null);
    const [canSolve, setCanSolve] = useState(true);

    useEffect(() => {
        // Server has a threading bug ideally would call all at the same time
        matrixServiceGetByName("Default").then((result) => {
            setMatrixData(JSON.parse(result.data.serializedMatrix));
            setMatrixId(result.data.id);
            matrixServiceGetAll().then((result) => {
                setMatrixSavedData(result?.data?.values ?? []);
                puzzleServiceGetKnightMovesResults().then((results) => {
                    setSolvedResults(results?.data ?? []);
                });
            });
        });
    }, []);

    const loadMatrix = (name) => {
        matrixServiceGetByName(name).then((result) => {
            setMatrixData(JSON.parse(result.data.serializedMatrix));
            setMatrixId(result.data.id);
        });
    };

    const saveMatrix = (name) => {
        const rowCount = matrixData.length;
        const columnCount = matrixData[0] ? matrixData[0].length : 0;
        const serializedMatrix = JSON.stringify(matrixData);
        const matrix = { name: name, rows: rowCount, columns: columnCount, serializedMatrix: serializedMatrix };

        matrixServicePut(matrix).then(() => {
            console.log("Matrix saved successfully");
            matrixServiceGetAll().then((result) => {
                setMatrixSavedData(result?.data?.values ?? []);
                loadMatrix(name);
            });
        });
    };

    const deleteMatrix = (matrixId) => {
        matrixServiceDelete(matrixId).then(() => {
            matrixServiceGetAll().then((result) => {
                setMatrixSavedData(result?.data?.values ?? []);
            });
        });
    };

    const handleCellChange = (rowIndex, colIndex, newValue) => {
        const newData = matrixData.map((row, rIndex) =>
            row.map((cell, cIndex) => (rIndex === rowIndex && cIndex === colIndex ? newValue : cell))
        );
        setMatrixData(newData);
    };

    const handleAddRow = () => {
        const newRow = new Array(matrixData[0].length).fill(" ");
        setMatrixData([...matrixData, newRow]);
    };

    const handleRemoveRow = (rowIndex) => {
        if (matrixData.length > 1) {
            setMatrixData(matrixData.filter((_, index) => index !== rowIndex));
        }
    };

    const handleAddColumn = () => {
        setMatrixData(matrixData.map((row) => [...row, " "]));
    };

    const handleRemoveColumn = (colIndex) => {
        if (matrixData[0].length > 1) {
            setMatrixData(matrixData.map((row) => row.filter((_, index) => index !== colIndex)));
        }
    };

    const handleSolve = () => {
        setCanSolve(false);

        const job = {
            description: "Knight Moves - Job",
            status: 0,
            puzzle: 0,
        };

        jobServicePut(job, matrixId).then(() => {
            console.log("jobStarted");
            handleRefresh();
            setTimeout(() => {
                setCanSolve(true);
            }, 5000);
        });
    };

    const handleRefresh = () => {
        puzzleServiceGetKnightMovesResults().then((results) => {
            setSolvedResults(results?.data ?? []);
        });
    };

    const handleResultDelete = (id) => {
        puzzleServiceDeleteKnightMovesResult(id).then(() => {
            handleRefresh();
        });
    };

    const renderCell = (cell, rowIndex, colIndex) => {
        if (cell === " ") {
            return null;
        }
        return <span>{cell}</span>;
    };

    const cellClassName = (cell, rowIndex, colIndex) => {
        return cell === " " ? "empty-cell" : "";
    };

    return (
        <div className="matrix-example">
            <h1>Knight Moves Challenge</h1>
            <div className="matrix-container1">
                <div className="matrix-wrapper">
                    <Matrix
                        data={matrixData}
                        onCellChange={handleCellChange}
                        onAddRow={handleAddRow}
                        onRemoveRow={handleRemoveRow}
                        onAddColumn={handleAddColumn}
                        onRemoveColumn={handleRemoveColumn}
                        renderCell={renderCell}
                        cellClassName={cellClassName}
                    />
                </div>
                <div className="table-wrapper">
                    <MatrixLoadSave
                        matrices={matrixSavedData}
                        onLoad={loadMatrix}
                        onSave={saveMatrix}
                        onDelete={deleteMatrix}
                    />
                </div>
            </div>
            <div className="container2">
                <button className="solve-button" onClick={handleSolve} disabled={!canSolve}>
                    {canSolve ? "Solve" : "Please wait..."}
                </button>
                <button className="refresh-button" onClick={handleRefresh}>
                    <FiRefreshCcw />
                </button>
            </div>
            <div className="container2">
                <div className="table-wrapper">
                    {" "}
                    <KnightsMoveTable results={solveResults} onDelete={handleResultDelete} />
                </div>
            </div>
        </div>
    );
};
