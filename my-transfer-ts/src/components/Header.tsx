import {Link, useNavigate} from "react-router-dom";
import {useAppDispatch, useAppSelector} from "../store";
import APP_ENV from "../env";
import {logout} from "../services/authSlice.ts";
// import {useSelector} from "react-redux";

export default function Header() {
    const user =
        useAppSelector(redux => redux.auth.user);
    // const user = useSelector
    // console.log("User auth", user);
    const appDispatch = useAppDispatch();
    const navigate = useNavigate();
    return (
        <header className="p-4 bg-blue-600 text-white">
            <nav className="flex gap-4 justify-between">
                <div className="flex items-center gap-4">
                    <Link to="/" className="hover:underline">Home</Link>
                    <Link to="/about" className="hover:underline">About</Link>
                </div>


                {
                    user!=null  ? (<div className={"flex justify-end gap-4"}>
                            <Link to="/user/Profile" className="hover:underline flex items-center h-full">
                                <div className="flex items-center justify-end gap-4">

                                    <img src={`${APP_ENV.API_BASE_URL}/images/${user ? user.image : "default.png"}`} alt="logo"
                                         className={"rounded-full w-8 h-8"}/>

                                    <h1 className={"text-xl"}>{`${user?.name}`}</h1>
                                </div>
                            </Link>

                            <span onClick={() => {
                                appDispatch(logout());
                                navigate("/");
                            }} className="px-6 py-2 cursor-pointer">Вийти</span>
                        </div>)
                        :
                        <div className={"flex justify-start gap-4"}>
                            <Link to="/user/register" className="hover:underline">Register</Link>
                            <Link to="/user/login" className="hover:underline">Log in</Link>
                        </div>

                }
            </nav>
        </header>
    );
}
