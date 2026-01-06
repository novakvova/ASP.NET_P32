import React, { useState } from "react";
import axios from "axios";
import ENV from "../../env/index";
import {Link, useNavigate} from "react-router-dom";
import type {Login} from "../../Interfaces/User/Login.ts";
// import { jwtDecode } from 'jwt-decode';
import type {LoginSuccess} from "../../Interfaces/User/LoginSuccess.ts";
// import type {UserTokenInfo} from "../../Interfaces/User/UserTokenInfo.ts";
import {loginSuccess} from "../../services/authSlice.ts";
import {useAppDispatch} from "../../store";
import {useGoogleLogin} from "@react-oauth/google";
import APP_ENV from "../../env/index";
// import {useDispatch} from "react-redux";

const LogIn: React.FC = () => {
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [errors, setErrors] = useState<Record<string, string[]>>({});


    //Давати повідмлення у Redux, що потірбно викнати певну дію
    const appDispatch = useAppDispatch();
    // const dispatch = useDispatch();

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
            appDispatch(loginSuccess(token));
            navigate("/profile");
        } catch (err) {
            if (axios.isAxiosError(err) && err.response?.data?.errors) {
                setErrors(err.response.data.errors);
            } else if (axios.isAxiosError(err) && err.response?.data?.message) {
                setErrors({ General: [err.response.data.message] });
            }
            console.error("Error:", err);
            alert("Log in failed");
        }

    };


    // const handleGoogleLogin = async (credentialResponse: CredentialResponse) => {
    //     try {
    //         const idToken = credentialResponse.credential;
    //         const response = await axios.post(`${APP_ENV.API_BASE_URL}/api/Account/googleLogin`, {
    //             idToken
    //         });
    //         console.log("Google користувач:", response.data);
    //     } catch (error) {
    //         console.error("Google логін не вдалий:", error);
    //     }
    // };

    const loginUseGoogle = useGoogleLogin({
        onSuccess: async (tokenResponse) =>
        {
            console.log("tokenResponse", tokenResponse.access_token);
            //console.log("tokenResponse", tokenResponse);
            try {

                const response =
                    await axios.post<LoginSuccess>(`${APP_ENV.API_BASE_URL}/api/Account/googleLogin`, {
                    idToken: tokenResponse.access_token
                });
                // dispatch(loginSuccess(result.token));
                const { token } = response.data;
                appDispatch(loginSuccess(token));
                navigate("/");
                console.log("Google користувач:", response.data);
            } catch (error) {
                console.error("Google логін не вдалий:", error);
            }
        },
    });

    return (
        <>
            <div style={{ maxWidth: "400px", margin: "0 auto", padding: "20px" }}>
                <form
                    onSubmit={handleSubmit}
                    className="bg-white shadow-lg rounded-xl p-8 w-full max-w-md border border-gray-200"
                >
                    <h2 className="text-2xl font-bold mb-6 text-center text-gray-800">
                        Логін
                    </h2>

                    {errors.email && <p className="text-red-600 text-sm mt-1">{errors.email[0]}</p>}

                    <div className="mb-5">
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Емейл
                        </label>
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            name="email"
                            placeholder="Емейл"
                            className="w-full border border-gray-300 rounded-lg px-4 py-2 focus:ring-2 focus:ring-green-400 focus:border-green-400 transition"
                        />
                        {errors.Email && (
                            <p className="text-red-600 text-sm mt-1">{errors.Email[0]}</p>
                        )}
                    </div>

                    <div className="mb-8">
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Пароль
                        </label>
                        <input
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            name="password"
                            placeholder="Пароль"
                            className="w-full border border-gray-300 rounded-lg px-4 py-2 focus:ring-2 focus:ring-green-400 focus:border-green-400 transition"
                        />
                        {errors.Password && (
                            <p className="text-red-600 text-sm mt-1">{errors.Password[0]}</p>
                        )}
                    </div>

                    <button
                        type="submit"
                        className="w-full py-3 px-4 bg-green-600 text-white rounded-lg hover:bg-green-700 transition font-semibold shadow"
                    >
                        Зберегти
                    </button>
                    <Link
                        to="/forgot-password"
                        className="text-blue-500 hover:text-blue-800 transition font-medium items-center"
                    >
                        Забули пароль
                    </Link>
                </form>
                <hr style={{ margin: "20px 0" }} />

                <button
                    onClick={(event) => {
                        event.preventDefault();
                        loginUseGoogle();
                    }}
                    className="bg-blue-500 hover:bg-blue-600 transition text-white font-semibold px-4 py-2 rounded w-full mt-4"
                >
                    {'LoginGoogle'}
                </button>
            </div>
        </>
    );
};

export default LogIn;