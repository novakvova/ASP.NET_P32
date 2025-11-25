import {useQuery} from "@tanstack/react-query";
import axios from "axios";
import type {ICountry} from "../Interfaces/ICountry.ts";
import {useState} from "react";
import Modal from "../Modals/Country"
import APP_ENV from "../env";

const Home = () =>{
    const api = `${APP_ENV.API_BASE_URL}/api`;
    const [selected, setSelected] = useState<ICountry | null>(null);
    const [isOpen, setIsOpen] = useState(false);


    function openModal(item: ICountry){
        setSelected(item);
        setIsOpen(true);
    }
    function closeModal(){
        setIsOpen(false);
        setSelected(null);
    }


    const {data, isLoading, error} = useQuery<ICountry[]>({
        queryKey: ["countries"],
        queryFn: async () => {
            const res = await axios.get(api + "/countries");
            return res.data;
        }
    });
    console.log(data);

    if(error) return <p className={"text-4xl text-center text-red-800"}>Error loading Users</p>
    if(isLoading) return <p className={"text-4xl text-center text-blue-500"}>Loading...</p>
    if(data === null) return <p className={"text-4xl text-center text-red-800"}>No users found</p>

    return (
        <div className={"w-full p-4 flex flex-row flex-wrap gap-4 justify-center"}>
            {
                data?.map((item, index:number) => (
                    <div
                        key={index}
                        className="max-w-sm bg-white rounded-xl shadow-md overflow-hidden border border-gray-200"
                    >
                        <img
                            src={`${APP_ENV.API_BASE_URL}/images/${item.image}`}
                            alt={item.name}
                            className="w-full h-48 object-cover"
                        />

                        <div className="p-6 text-center">

                        <span className="inline-block px-2 py-1 text-xs font-medium bg-blue-100 text-blue-700 rounded-md">
                          {item.slug}
                        </span>

                            <h3 className="mt-4 text-2xl font-semibold text-gray-900">
                                {item.name}
                            </h3>

                            <p className="mt-2 text-gray-600 text-sm">
                                {item.description}
                            </p>

                            <button
                                onClick={() => openModal(item)}
                                className="mt-6 inline-flex items-center px-4 py-2 bg-blue-600 text-white rounded-md text-sm font-medium hover:bg-blue-700 transition"
                            >
                                Read more →
                            </button>

                        </div>
                    </div>

                ))
            }

            <Modal
                isOpen={isOpen}
                onClose={closeModal}
                country={selected}
            />
        </div>
    )
};
export default Home;