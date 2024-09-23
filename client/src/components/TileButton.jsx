import React from "react";
import { useNavigate } from "react-router-dom";
import PropTypes from "prop-types";
import "./tileButton.css";

export const TileButton = ({ icon: Icon, title, route }) => {
    const navigate = useNavigate();

    const handleClick = () => {
        navigate(route);
    };

    return (
        <button onClick={handleClick} className="tile-button">
            <Icon className="tile-icon" />
            <span className="tile-title">{title}</span>
        </button>
    );
};

TileButton.propTypes = {
    icon: PropTypes.elementType.isRequired,
    title: PropTypes.string.isRequired,
    route: PropTypes.string.isRequired,
};
