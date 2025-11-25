
import type { ICountry } from "../Interfaces/ICountry";

interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
    country: ICountry | null;
}

export default function Country({ isOpen, onClose, country }: ModalProps) {
    if (!isOpen || !country) return null;

    const url = "http://localhost:5055";

    return (
        <div className="fixed inset-0 bg-black/40  flex items-center justify-center z-50"
             onClick={onClose}
        >
            <div className="bg-white rounded-xl shadow-lg max-w-lg w-full p-6 relative">

                {/* Close Button */}
                <button
                    onClick={onClose}
                    className="absolute top-3 right-3 text-gray-500 hover:text-gray-800 text-xl"
                >
                    ×
                </button>

                <img
                    src={`${url}/images/${country.image}`}
                    alt={country.name}
                    className="w-full h-56 object-cover rounded-lg"
                />
                <h2 className="mt-4 text-3xl font-bold text-gray-900 text-center">
                    {country.name}
                </h2>

                <p className="text-center text-sm text-gray-500">{country.slug}</p>

                <p className="mt-4 text-gray-700 leading-relaxed">
                    {country.description}
                </p>


                {/* Tags */}
                <div className="mt-4 flex flex-wrap gap-2">
                    {country.tags.map((tag, i) => (
                        <span
                            key={i}
                            className="bg-blue-100 text-blue-700 px-2 py-1 rounded-md text-xs font-medium"
                        >
              #{tag}
            </span>
                    ))}
                </div>

            </div>
        </div>
    );
}
