import React, { useEffect } from "react";
import { FaChessKnight, FaGithub } from "react-icons/fa";
import { IoIosConstruct } from "react-icons/io";
import "./dashboard.css";
import { TileButton } from "@components/TileButton";

export const Dashboard = () => {
    return (
        <div className="dashboard-wrapper">
            <div className="dashboard-card">
                <h1 className="dashboard-title">Puzzles Dashboard</h1>
                <div className="dashboard-grid">
                    <TileButton icon={FaChessKnight} title="Knight Moves" route="/knight-moves" />
                    <TileButton icon={IoIosConstruct} title="TBA" route="/tba" />
                    <TileButton icon={IoIosConstruct} title="TBA" route="/tba" />
                    <TileButton icon={IoIosConstruct} title="TBA" route="/tba" />
                </div>
                <div className="footer-wrapper">
                    <a
                        href="https://github.com/jspauldo98/puzzles"
                        target="_blank"
                        rel="noopener noreferrer"
                        className="source-code-link"
                    >
                        <FaGithub className="github-icon" />
                        Source Code
                    </a>
                </div>
            </div>
        </div>
    );
};
