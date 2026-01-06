import {useNavigate} from "react-router-dom";

function RedirectBtn() {
    const navigate = useNavigate();
    return (
        <div className="flex justify-center mb-6">
            <button
                onClick={() => navigate("/admin")}
                className="px-6 py-2 rounded-lg bg-green-600 text-white font-semibold shadow hover:bg-green-700 transition"
            >
                Перейти на адмін панель
            </button>
        </div>
    );
};

export default RedirectBtn;