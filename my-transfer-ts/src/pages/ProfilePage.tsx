import {useEffect, useState} from "react";
import axios from "axios";

import APP_ENV from "../env";
import type {IUserProfile} from "../Interfaces/User/IUserProfile.ts";

function ProfilePage() {
    const [user, setUser] = useState<IUserProfile | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const token = localStorage.getItem("token");
                const response = await axios.get<IUserProfile>(
                    `${APP_ENV.API_BASE_URL}/api/Account/GetProfile`,
                    {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        }
                    }
                );
                setUser(response.data);
            } catch (error) {
                console.error("Помилка при отриманні міст:", error);
            } finally {
                setLoading(false);
            }
        };
        fetchUser();
    }, []);
    if (loading) {
        return <p>Завантаження...</p>
    }
    if (!user) {
        return <p>Не вдалось отримати данні користувача</p>
    }

    return (
        <div className="max-w-md mx-auto bg-white dark:bg-slate-900 shadow-lg rounded-xl p-8">
            <h2 className="text-2xl font-bold mb-6 text-center text-gray-800 dark:text-white">
                Профіль користувача
            </h2>
            <div className="flex justify-center mb-6">
                <img src={`${APP_ENV.API_BASE_URL}/Images/${user.image}`}
                     alt="User avatar"
                     className="w-32 h-32 rounded-full ring-2 ring-blue-500 object-cover"
                />
            </div>
            <div className="space-y-4">
                <div>
                    <label className="block text-sm font-medium text-gray-700 dark:text-slate-300 mb-1">
                        ID
                    </label>
                    <input type="text"
                           value={user.id}
                           readOnly
                           className="w-full border border-gray-300 dark:border-slate-600 rounded-lg px-4 py-2 bg-gray-100 dark:bg-slate-800 dark:text-white cursor-not-allowed"
                    />
                </div>
                <div>
                    <label className="block text-sm font-medium text-gray-700 dark:text-slate-300 mb-1">
                        Ім’я
                    </label>
                    <input type="text"
                           value={user.fullName}
                           readOnly
                           className="w-full border border-gray-300 dark:border-slate-600 rounded-lg px-4 py-2 bg-gray-100 dark:bg-slate-800 dark:text-white cursor-not-allowed"
                    />
                </div>
                <div>
                    <label className="block text-sm font-medium text-gray-700 dark:text-slate-300 mb-1">
                        Email
                    </label>
                    <input type="email"
                           value={user.email}
                           readOnly
                           className="w-full border border-gray-300 dark:border-slate-600 rounded-lg px-4 py-2 bg-gray-100 dark:bg-slate-800 dark:text-white cursor-not-allowed"
                    />
                </div>
                <div>
                    <label className="block text-sm font-medium text-gray-700 dark:text-slate-300 mb-1">
                        Телефон
                    </label>
                    <input type="text"
                           value={user.phone}
                           readOnly
                           className="w-full border border-gray-300 dark:border-slate-600 rounded-lg px-4 py-2 bg-gray-100 dark:bg-slate-800 dark:text-white cursor-not-allowed"
                    />
                </div>
            </div>
        </div>);
}

export default ProfilePage;