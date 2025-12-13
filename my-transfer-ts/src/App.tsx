import Header from "./components/Header";
import { Outlet } from "react-router-dom";
import {useAppSelector} from "./store";

export default function App() {
    const user =
        useAppSelector(redux => redux.auth.user);

    console.log("User roles", user?.roles);
    return (
        <div className="min-h-screen bg-gray-100">
            <Header />

            <main className="p-6">
                <Outlet />
            </main>
        </div>
    );
}
