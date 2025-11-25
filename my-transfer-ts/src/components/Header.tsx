import { Link } from "react-router-dom";

export default function Header() {
    return (
        <header className="p-4 bg-blue-600 text-white">
            <nav className="flex gap-4">
                <Link to="/" className="hover:underline">Home</Link>
                <Link to="/about" className="hover:underline">About</Link>
            </nav>
        </header>
    );
}
