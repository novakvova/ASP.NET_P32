import Header from "./components/Header";
import { Outlet } from "react-router-dom";

export default function App() {
    return (
        <div className="min-h-screen bg-gray-100">
            <Header />

            <main className="p-6">
                <Outlet />
            </main>
        </div>
    );
}
