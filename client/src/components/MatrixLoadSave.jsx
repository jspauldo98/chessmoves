import React, { useState } from "react";

export const MatrixLoadSave = ({ matrices, onLoad, onSave, onDelete }) => {
    const [newMatrixName, setNewMatrixName] = useState("");

    const handleLoad = (matrixName) => {
        onLoad(matrixName);
    };

    const handleDelete = (matrixId) => {
        onDelete(matrixId);
    };

    const handleSave = () => {
        if (newMatrixName.trim() !== "") {
            onSave(newMatrixName);
            setNewMatrixName("");
        }
    };

    return (
        <div>
            <h2>Load/Save Matrix</h2>
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {matrices.map((matrix) => (
                        <tr key={matrix.id}>
                            <td>{matrix.name}</td>
                            <td>
                                <button onClick={() => handleLoad(matrix.name)}>Load</button>
                                <button onClick={() => handleDelete(matrix.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <div>
                <input
                    type="text"
                    value={newMatrixName}
                    onChange={(e) => setNewMatrixName(e.target.value)}
                    placeholder="New matrix name"
                />
                <button onClick={handleSave}>Save</button>
            </div>
        </div>
    );
};
