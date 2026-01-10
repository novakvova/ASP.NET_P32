import { useEffect, useState } from "react";
import axios from "axios";
import APP_ENV from "../env";
import type {ITransportations} from "../Interfaces/Transportation/ITransportations.ts";
import type {ICartAddUpdate} from "../Interfaces/Cart/ICartAddUpdate.ts";
import type {ICartItem} from "../Interfaces/Cart/ICartItem.ts";
import {useNavigate} from "react-router-dom";
import {useAppSelector} from "../store";

function TransportationsPage() {
    const [pages, setPages] = useState<ITransportations[]>([]);
    const token = localStorage.getItem("token");
    const navigate = useNavigate();
    useEffect(() => {
        const fetchTransportations = async () => {
            try {
                const response = await axios.get<ITransportations[]>(`${APP_ENV.API_BASE_URL}/api/Transportations/GetList`, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                setPages(response.data);
            } catch (error) {
                console.error("Помилка при отриманні квитків:", error);
            }
        };
        fetchTransportations();
    }, []);

    const user = useAppSelector(redux => redux.auth.user);
    const getStatusColor = (status: string) => {
        switch (status) {
            case "Запланований":
                return "bg-green-100 text-green-700 dark:bg-green-900 dark:text-green-300";
            case "Затримується":
                return "bg-orange-100 text-orange-700 dark:bg-orange-900 dark:text-orange-300";
            case "Скасований":
            case "Виконаний":
            case "Немає місць":
                return "bg-red-100 text-red-700 dark:bg-red-900 dark:text-red-300";
            default: return "bg-gray-100 text-gray-700 dark:bg-gray-800 dark:text-gray-300";
        }
    };

    const isBookable = (status: string) => {
        return !(status === "Скасований" || status === "Виконаний" || status === "Немає місць");
    };

    const handleGoLogin = () => {
        navigate("/login");
    }

    const handleAddUpdate = async (transportationId: number) => {
        try {
            const { data: cartItems } = await axios.get<ICartItem[]>(
                `${APP_ENV.API_BASE_URL}/api/carts/getList`,
                { headers: { Authorization: `Bearer ${token}` } }
            );

            const existingItem = cartItems.find(item => item.id === transportationId);

            const newQuantity = existingItem ? existingItem.quantity + 1 : 1;

            const model: ICartAddUpdate = {
                transportationId,
                quantity: newQuantity
            };

            await axios.post(`${APP_ENV.API_BASE_URL}/api/Carts/AddUpdate`, model, {
                headers: { Authorization: `Bearer ${token}` },
            });

            alert(existingItem ? "Кількість квитків збільшено" : "Квиток додано у кошик");
        } catch (err) {
            console.error("Помилка при додаванні квитка в кошик:", err);
        }
    };

    return (
        <div className="p-10 bg-transparent min-h-screen">
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-10">
                {pages.map(transportation => (
                    <div
                        key={transportation.id}
                        className="bg-white dark:bg-slate-900 rounded-2xl shadow-xl border border-slate-200 dark:border-slate-700 overflow-hidden transition hover:shadow-2xl hover:scale-[1.02]"
                    >
                        <div className="p-6 space-y-4">
                            {/* Маршрут */}
                            <div className="flex items-center justify-between">
                                <h2 className="text-xl font-bold text-gray-800 dark:text-white">
                                    {transportation.fromCityName} → {transportation.toCityName}
                                </h2>
                                <span className={`text-sm px-3 py-1 rounded-full font-semibold ${getStatusColor( transportation.statusName )}`} >
                                    {transportation.statusName}
                                </span>
                            </div>

                            <div className="grid grid-cols-2 gap-4 text-sm text-gray-700 dark:text-slate-300">
                                <div>
                                    <p className="font-semibold">Відправлення</p>
                                    <p>{transportation.departureTime}</p>
                                </div>
                                <div>
                                    <p className="font-semibold">Прибуття</p>
                                    <p>{transportation.arrivalTime}</p>
                                </div>
                            </div>

                            <div className="grid grid-cols-2 gap-4 text-sm text-gray-600 dark:text-slate-400">
                                <div>
                                    <p>Країна відправлення:</p>
                                    <p className="font-semibold">{transportation.fromCountryName}</p>
                                </div>
                                <div>
                                    <p>Країна прибуття:</p>
                                    <p className="font-semibold">{transportation.toCountryName}</p>
                                </div>
                            </div>

                            <div className="text-sm text-gray-700 dark:text-slate-300">
                                <p>
                                    Місць всього: <span className="font-semibold">{transportation.seatsTotal}</span> | Доступно:{" "}
                                    <span className="font-semibold">{transportation.seatsAvailable}</span>
                                </p>
                            </div>

                            <div className="flex items-center justify-between mt-4">
                                <p className="text-xl font-bold text-blue-600 dark:text-blue-400">
                                    {transportation.price} ₴
                                </p>
                                {user!=null ? (
                                    <>
                                        <button disabled={!isBookable(transportation.statusName)}
                                                className={`font-semibold py-2 px-5 rounded-full transition duration-300 ${isBookable(transportation.statusName) ?
                                                    "bg-blue-600 hover:bg-blue-700 text-white" : "bg-gray-400 text-gray-200 cursor-not-allowed"} `}
                                                onClick={() => handleAddUpdate(transportation.id)}
                                        >
                                            Додати в кошик
                                        </button>
                                    </>
                                ) : (
                                    <>
                                        <button disabled={!isBookable(transportation.statusName)}
                                                className={`font-semibold py-2 px-5 rounded-full transition duration-300 ${isBookable(transportation.statusName) ?
                                                    "bg-blue-600 hover:bg-blue-700 text-white" : "bg-gray-400 text-gray-200 cursor-not-allowed"} `}
                                                onClick={handleGoLogin}
                                        >
                                            Додати в кошик
                                        </button>
                                    </>
                                )}

                            </div>
                        </div>
                    </div>

                ))}
            </div>
        </div>
    );
}

export default TransportationsPage;