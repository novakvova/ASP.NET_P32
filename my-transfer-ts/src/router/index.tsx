import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../pages/Home";
import AboutPage from "../pages/About";
import LogIn from "../pages/Users/LogIn.tsx";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            { path: "", element: <HomePage /> },
            { path: "about", element: <AboutPage /> },
            { path: "user/LogIn", element: <LogIn/> },
        ],
    },
]);
