import {useEffect, useState} from "react";
import axios from "axios";
import APP_ENV from "../env";
import "react-datepicker/dist/react-datepicker.css";
import {ArrowLeft, ArrowLeftToLine, ArrowRight, ArrowRightToLine} from "lucide-react";
import {useNavigate, useLocation} from "react-router-dom";
import DatePicker from "react-datepicker";
import {format} from "date-fns";
import type {IAccountSearchResponse} from "../Interfaces/User/IAccountSearchResponse.ts";
import type {IAccountSearch} from "../Interfaces/User/IAccountSearch.ts";

function AllUsersFilterPage() {
    const [users, setUsers] = useState<IAccountSearch[]>([]);
    const [pagination, setPagination] = useState<IAccountSearchResponse["pagination"] | null>(null);
    const [currentPage, setCurrentPage] = useState(1);
    const [nameFilter, setNameFilter] = useState("");
    const [startDate, setStartDate] = useState<Date | null>(null);
    const [endDate, setEndDate] = useState<Date | null>(null);
    const [itemsPerPage, setItemsPerPage] = useState(10);

    const navigate = useNavigate();
    const location = useLocation();

    const fetchUsers = async (page: number) => {
        try {
            const response = await axios.get<IAccountSearchResponse>(
                `${APP_ENV.API_BASE_URL}/api/Account/Search`, {
                    params: {
                        page,
                        Name: nameFilter,
                        StartDate: startDate,
                        EndDate: endDate,
                        ItemPerPage: itemsPerPage
                    }
                }
            );
            setUsers(response.data.items);
            setPagination(response.data.pagination);
        } catch (error) {
            console.error("Помилка при отриманні користувачів:", error);
        }
    };

    const updateUrl = () => {
        const params = new URLSearchParams();
        params.set("page", currentPage.toString());
        if (nameFilter) params.set("Name", nameFilter);
        if (startDate) params.set("StartDate", startDate ? format(startDate, "yyyy-MM-dd") : "");
        if (endDate) params.set("EndDate", endDate ? format(endDate, "yyyy-MM-dd") : "");
        params.set("ItemPerPage", itemsPerPage.toString());
        navigate(`${location.pathname}?${params.toString()}`);
    };

    useEffect(() => {
        updateUrl();
        fetchUsers(currentPage);
    }, [currentPage, nameFilter, startDate, endDate, itemsPerPage]);

    useEffect(() => {
        const params = new URLSearchParams(location.search);
        const page = parseInt(params.get("page") || "1", 10);
        const name = params.get("Name") || "";
        const start = params.get("StartDate") || "";
        const end = params.get("EndDate") || "";
        const perPage = parseInt(params.get("ItemPerPage") || "10", 10);

        setCurrentPage(page);
        setNameFilter(name);
        setStartDate(start ? new Date(start) : null);
        setEndDate(end ? new Date(end) : null);
        setItemsPerPage(perPage);
    }, []);

    const handlePrev = () => {
        if (pagination && currentPage > 1) {
            setCurrentPage(prev => prev - 1);
        }
    };

    const handleNext = () => {
        if (pagination && currentPage < pagination.totalPages) {
            setCurrentPage(prev => prev + 1);
        }
    };

    return (
        <>
            <div className="sticky top-0 z-50 mb-6 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-4 items-end
                border border-gray-300 dark:border-slate-700 bg-white dark:bg-slate-900 rounded-xl p-6 shadow-md">

                <div className="flex flex-col">
                    <label className="text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                        Пошук по імені
                    </label>
                    <input type="text"
                           placeholder="Введіть ім'я..."
                           value={nameFilter}
                           onChange={e => {
                               setNameFilter(e.target.value);
                               setCurrentPage(1);
                           }}
                           className="w-full border border-gray-300 dark:border-slate-600 rounded-lg px-3 py-2
                            bg-gray-100 dark:bg-slate-800 dark:text-white focus:outline-none focus:ring-2 focus:ring-blue-500"
                    />
                </div>

                <div className="flex flex-col">
                    <label className="text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                        Дата початку
                    </label>
                    <DatePicker selected={startDate}
                                onChange={(date: Date | null) => {
                                    setStartDate(date);
                                    setCurrentPage(1);
                                }}
                                dateFormat="dd.MM.yyyy"
                                placeholderText="Оберіть дату..."
                                className="w-full border border-gray-300 dark:border-slate-600 rounded-lg px-3 py-2 bg-gray-100
                                 dark:bg-slate-800 dark:text-white focus:outline-none focus:ring-2 focus:ring-blue-500"
                    />
                </div>

                <div className="flex flex-col">
                    <label className="text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                        Дата кінця
                    </label>
                    <DatePicker selected={endDate}
                                onChange={(date: Date | null) => {
                                    setEndDate(date);
                                    setCurrentPage(1);
                                }}
                                dateFormat="dd.MM.yyyy"
                                placeholderText="Оберіть дату..."
                                className="w-full border border-gray-300 dark:border-slate-600 rounded-lg px-3 py-2 bg-gray-100
                                  dark:bg-slate-800 dark:text-white focus:outline-none focus:ring-2 focus:ring-blue-500"
                                calendarClassName="dark-calendar"
                    />
                </div>

                <div className="flex flex-col">
                    <label className="text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                        Елементів на сторінку
                    </label>
                    <input type="number"
                           min="1"
                           value={itemsPerPage}
                           onChange={e => {
                               const value = Number(e.target.value);
                               setItemsPerPage(value > 0 ? value : 10);
                               setCurrentPage(1);
                           }}
                           className="w-full border border-gray-300 dark:border-slate-600 rounded-lg px-3 py-2
                            bg-gray-100 dark:bg-slate-800 dark:text-white focus:outline-none focus:ring-2 focus:ring-blue-500"
                    />
                </div>

                <div className="flex flex-col justify-end">
                    <button onClick={() => {
                        setNameFilter("");
                        setStartDate(null);
                        setEndDate(null);
                        setItemsPerPage(10);
                        setCurrentPage(1);
                    }}
                            className="w-full py-3 px-4 bg-gradient-to-r from-blue-500 to-purple-600
                                text-white rounded-lg hover:shadow-lg transition-all border border-blue-600"
                    >
                        Очистити фільтри
                    </button>
                </div>

            </div>

            <div className="p-10 pt-0 bg-transparent min-h-screen ">

                <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-10">
                    {users.length === 0 ? (
                        <div className="col-span-full text-center text-gray-600 dark:text-slate-400 text-lg font-semibold">
                            Користувачі не знайдені...
                        </div>
                    ) : (
                        users.map(u => (
                            <div key={u.id}
                                 className="bg-white/80 dark:bg-slate-900/80 rounded-2xl shadow-xl overflow-hidden transform
                                        transition duration-500 hover:scale-105 hover:shadow-2xl border border-slate-200/50
                                        dark:border-slate-700/50 hover:shadow-slate-200/20 dark:hover:shadow-slate-900/20">
                                <div className="relative h-48 w-full ">
                                    <img src={`${APP_ENV.API_BASE_URL}/images/${u.image ?? "default.png"}`}
                                         alt={u.fullName}
                                         className="h-full w-full object-cover transition-transform duration-500 hover:scale-110"
                                    />
                                    <div className="absolute inset-0 bg-gradient-to-t from-black/40 to-transparent"></div>
                                    <h2 className="absolute bottom-3 left-4 text-xl font-bold text-white drop-shadow-lg">
                                        {u.fullName}
                                    </h2>
                                </div>
                                <div className="p-6 text-center">
                                    <p className="text-sm text-gray-600 dark:text-slate-400 mb-1">
                                        Ай ді: <span className="font-semibold">{u.id}</span>
                                    </p>
                                    <p className="text-sm text-gray-600 dark:text-slate-400 mb-4">
                                        Емейл: <span className="font-semibold">{u.email}</span>
                                    </p>
                                    <p className="text-sm text-gray-600 dark:text-slate-400 mb-4">
                                        Ролі: <span className="font-semibold">{u.roles}</span>
                                    </p>
                                </div>
                            </div>
                        ))
                    )}
                </div>

                {pagination && (
                    <div className="mt-6 text-center text-gray-500 dark:text-slate-400">
                        Сторінка {pagination.currentPage} з {pagination.totalPages}
                    </div>
                )}

                {pagination && (
                    <nav aria-label="Page navigation example"
                         className="mt-4 flex justify-center">
                        <ul className="flex -space-x-px text-sm">
                            <li>
                                <button onClick={() => setCurrentPage(1)}
                                        disabled={currentPage === 1}
                                        className="px-3 h-9 border rounded-s-base bg-white dark:bg-slate-800 text-gray-700
                                            dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-slate-700 disabled:opacity-50">
                                    <ArrowLeftToLine/>
                                </button>
                            </li>

                            <li>
                                <button onClick={handlePrev}
                                        disabled={currentPage === 1}
                                        className="px-3 h-9 border bg-white dark:bg-slate-800 text-gray-700 dark:text-gray-300
                                            hover:bg-gray-100 dark:hover:bg-slate-700 disabled:opacity-50">
                                    <ArrowLeft/>
                                </button>
                            </li>

                            {(() => {
                                const totalPages = pagination.totalPages;
                                const maxVisible = 10;

                                let startPage = 1;
                                let endPage = maxVisible;

                                if (currentPage > 6) {
                                    startPage = currentPage - 5;
                                    endPage = startPage + maxVisible - 1;
                                }

                                if (endPage > totalPages) {
                                    endPage = totalPages;
                                    startPage = Math.max(1, endPage - maxVisible + 1);
                                }

                                const pages = [];
                                for (let i = startPage; i <= endPage; i++) {
                                    pages.push(
                                        <li key={i}>
                                            <button onClick={() => setCurrentPage(i)}
                                                    className={`w-9 h-9 border ${i === currentPage
                                                        ? "bg-blue-500 text-white"
                                                        : "bg-white dark:bg-slate-800 text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-slate-700"
                                                    }`}
                                            >
                                                {i}
                                            </button>
                                        </li>
                                    );
                                }
                                return pages;
                            })()}

                            <li>
                                <button onClick={handleNext}
                                        disabled={currentPage === pagination.totalPages}
                                        className="px-3 h-9 borderbg-white dark:bg-slate-800 text-gray-700 dark:text-gray-300
                                            hover:bg-gray-100 dark:hover:bg-slate-700 disabled:opacity-50">
                                    <ArrowRight/>
                                </button>
                            </li>

                            <li>
                                <button
                                    onClick={() => setCurrentPage(pagination.totalPages)}
                                    disabled={currentPage === pagination.totalPages}
                                    className="px-3 h-9 border rounded-e-base bg-white dark:bg-slate-800 text-gray-700
                                            dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-slate-700 disabled:opacity-50">
                                    <ArrowRightToLine/>
                                </button>
                            </li>
                        </ul>
                    </nav>
                )}
            </div>
        </>
    );
}

export default AllUsersFilterPage;