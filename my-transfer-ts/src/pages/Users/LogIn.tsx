import React, { useState } from "react";
import axios from "axios";
import ENV from "../../env/index";
import {useNavigate} from "react-router-dom";
import type {Login} from "../../Interfaces/User/Login.ts";
// import { jwtDecode } from 'jwt-decode';
import type {LoginSuccess} from "../../Interfaces/User/LoginSuccess.ts";
// import type {UserTokenInfo} from "../../Interfaces/User/UserTokenInfo.ts";
import {loginSuccess} from "../../services/authSlice.ts";
import {useAppDispatch} from "../../store";

const LogIn: React.FC = () => {
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    //Давати повідмлення у Redux, що потірбно викнати певну дію
    const appDispatch = useAppDispatch();

    const navigate = useNavigate();


    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const model:Login = {
            email,
            password
        }

        try {
            const res = await axios.post<LoginSuccess>(
                ENV.API_BASE_URL + "/api/account/login",
                model,
            );
            const { token } = res.data;
            //Повідомляємо, що користувач пройшов процедуру входу успішно
            appDispatch(loginSuccess(token));
            //const decode = jwtDecode<UserTokenInfo>(token);
            //console.log("Decode token", decode);
            // console.log("Decode token", decode.roles);
            //
            // console.log("Registered:", res.data.token);
            //localStorage.token = token;
            //localStorage.setItem("token", token);
            // alert("Log in successful!");
            navigate("/");
        } catch (err) {
            console.error("Error:", err);
            alert("Log in failed");
        }

    };

    return (
        <div className="h-screen w-full bg-gradient-to-br from-indigo-300 via-purple-300 to-pink-300 flex justify-center items-center">
            <form
                onSubmit={handleSubmit}
                className="mx-auto max-w-lg space-y-4 rounded-lg border border-gray-300 bg-gray-100 p-6"
            >




                {/* EMAIL */}
                <div>
                    <label className="block text-sm font-medium text-gray-900">
                        Email
                    </label>
                    <input
                        className="mt-1 w-full rounded-lg border-gray-300"
                        type="email"
                        placeholder="Your email"
                        value={email}
                        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                            setEmail(e.target.value)
                        }
                    />
                </div>

                {/* PASSWORD */}
                <div>
                    <label className="block text-sm font-medium text-gray-900">
                        Password
                    </label>
                    <input
                        className="mt-1 w-full rounded-lg border-gray-300"
                        type="password"
                        placeholder="Your password"
                        value={password}
                        onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
                            setPassword(e.target.value)
                        }
                    />
                </div>


                {/* SUBMIT BUTTON */}
                <button
                    className="block w-full rounded-lg border border-indigo-600 bg-indigo-600 px-12 py-3 text-sm font-medium text-white transition-colors hover:bg-transparent hover:text-indigo-600"
                    type="submit"
                >
                    Log in
                </button>
            </form>
        </div>
    );
};

export default LogIn;