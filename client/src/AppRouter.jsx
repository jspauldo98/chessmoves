import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { Dashboard } from "@layouts/Dashboard";
import { Tba } from "@layouts/Tba";
import { NotFound } from "@layouts/NotFound";
import { KnightMoves } from "@layouts/KnightMoves";

export const AppRouter = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Dashboard />}></Route>
                <Route path="/*" element={<NotFound />}></Route>
                <Route path="/tba" element={<Tba />}></Route>
                <Route path="/knight-moves" element={<KnightMoves />}></Route>
            </Routes>
        </Router>
    );
};
