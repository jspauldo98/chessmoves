import React, { useState } from "react";
import PropTypes from "prop-types";
import { FaPlus, FaMinus } from "react-icons/fa";
import "./matrix.css";

export const Matrix = ({
    data,
    onCellChange,
    onAddRow,
    onRemoveRow,
    onAddColumn,
    onRemoveColumn,
    renderCell,
    cellClassName,
}) => {
    const [editingCell, setEditingCell] = useState(null);
    const [editValue, setEditValue] = useState("");

    const handleCellClick = (rowIndex, colIndex) => {
        setEditingCell({ rowIndex, colIndex });
        setEditValue(data[rowIndex][colIndex]);
    };

    const handleInputChange = (e) => {
        setEditValue(e.target.value.trim());
    };

    const handleInputBlur = () => {
        if (editingCell) {
            onCellChange(editingCell.rowIndex, editingCell.colIndex, editValue);
            setEditingCell(null);
        }
    };

    const handleInputKeyDown = (e) => {
        if (e.key === "Enter") {
            handleInputBlur();
        }
    };

    const isEmpty = data.length === 0 || data[0].length === 0;

    return (
        <div className="matrix-container">
            <button onClick={onAddRow} className="add-button add-row">
                <FaPlus size={15} />
            </button>
            <div className="matrix-content">
                <div className="matrix-with-buttons">
                    <div className="vertical-button-container">
                        <button
                            onClick={() => onRemoveColumn(data[0].length - 1)}
                            className="remove-button remove-column"
                        >
                            <FaMinus size={15} />
                        </button>
                    </div>
                    <div className="matrix">
                        {!isEmpty ? (
                            data.map((row, rowIndex) => (
                                <div key={rowIndex} className="matrix-row">
                                    {row.map((cell, colIndex) => (
                                        <div
                                            key={`${rowIndex}-${colIndex}`}
                                            className={`matrix-cell ${
                                                cellClassName ? cellClassName(cell, rowIndex, colIndex) : ""
                                            }`}
                                            onClick={() => handleCellClick(rowIndex, colIndex)}
                                        >
                                            {editingCell &&
                                            editingCell.rowIndex === rowIndex &&
                                            editingCell.colIndex === colIndex ? (
                                                <input
                                                    type="text"
                                                    value={editValue}
                                                    onChange={handleInputChange}
                                                    onBlur={handleInputBlur}
                                                    onKeyDown={handleInputKeyDown}
                                                    autoFocus
                                                />
                                            ) : renderCell ? (
                                                renderCell(cell, rowIndex, colIndex)
                                            ) : (
                                                cell
                                            )}
                                        </div>
                                    ))}
                                </div>
                            ))
                        ) : (
                            <div className="empty-matrix-message">Matrix is empty</div>
                        )}
                    </div>
                    <div className="vertical-button-container">
                        <button onClick={onAddColumn} className="add-button add-column">
                            <FaPlus size={15} />
                        </button>
                    </div>
                </div>
            </div>
            <button onClick={() => onRemoveRow(data[0].length - 1)} className="remove-button remove-row">
                <FaMinus size={15} />
            </button>
        </div>
    );
};

Matrix.propTypes = {
    data: PropTypes.arrayOf(PropTypes.array).isRequired,
    onCellChange: PropTypes.func.isRequired,
    onAddRow: PropTypes.func.isRequired,
    onRemoveRow: PropTypes.func.isRequired,
    onAddColumn: PropTypes.func.isRequired,
    onRemoveColumn: PropTypes.func.isRequired,
    renderCell: PropTypes.func,
    cellClassName: PropTypes.func,
};
