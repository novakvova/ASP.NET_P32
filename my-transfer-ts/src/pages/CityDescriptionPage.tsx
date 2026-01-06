import { useEffect, useState } from "react";
import {useLocation, useNavigate} from "react-router-dom";
import axios from "axios";
import APP_ENV from "../env";
import type { ICity } from "../Interfaces/City/ICity.ts";

function CityDescriptionPage() {
    const location = useLocation();
    const id = location.state.id
    const navigate = useNavigate();
    const [city, setCity] = useState<ICity | null>(null);

    useEffect(() => {
        if (!id) return;
        const fetchCity = async () => {
            try {
                const response = await axios.get<ICity>(`${APP_ENV.API_BASE_URL}/api/Cities/${id}`);
                setCity(response.data);
            } catch (error) {
                console.error("Помилка при отриманні міста:", error);
            }
        };
        fetchCity();
    }, [id]);

    if (!city) {
        return <p className="text-center mt-10 text-gray-600">Завантаження...</p>;
    }

    return (

        <div className="min-h-screen p-10 justify-self-center">
            <div
                className="bg-white dark:bg-slate-900 rounded-xl shadow-lg max-w-3xl p-6 overflow-hidden"
            >
                <div
                    className="prose max-w-none prose-img:rounded-lg prose-img:mx-auto prose-img:max-w-full dark:text-white"
                    dangerouslySetInnerHTML={{ __html: city.description }}
                />
            </div>
            <div className="flex justify-center mt-8">
                <button
                    onClick={() => navigate(-1)}
                    className="px-6 py-2 rounded-lg bg-green-600 text-white font-semibold hover:bg-green-700 transition"
                >
                    Повернутись назад
                </button>
            </div>
        </div>
    );
}

export default CityDescriptionPage;