import { useState } from 'react';
// import { useAuth } from '../context/AuthContext';
import { useNavigate, Link } from 'react-router-dom';
import {Formik, Form, Field, ErrorMessage, type FormikHelpers} from 'formik';
import * as yup from 'yup';
import type {IRegisterModel} from "../../Interfaces/User/IRegisterModel.tsx";
import APP_ENV from "../../env";
import {loginSuccess} from "../../services/authSlice.ts";
import {useAppDispatch} from "../../store";
import axios from "axios";

const phoneRegExp = /^\+380\d{9}$/;

const RegisterSchema = yup.object({
    firstName: yup.string().required("Введіть ім'я"),
    lastName: yup.string().required("Введіть прізвище"),
    email: yup.string().email("Некоректна пошта").required("Введіть пошту"),
    password: yup.string().required("Введіть пароль").min(6, "Мінімум 6 символів"),
    confirmPassword: yup.string()
        .oneOf([yup.ref('password')], 'Паролі не співпадають')
        .required('Підтвердіть пароль'),
    phone: yup.string()
        .matches(phoneRegExp, 'Невірний формат')
        .required("Введіть номер телефону"),
});

const RegisterPage = () => {
    // const { register } = useAuth();
    const navigate = useNavigate();
    const [preview, setPreview] = useState<string | null>(null);

    const appDispatch = useAppDispatch();

    const handleSubmit = async (values: IRegisterModel, { setSubmitting }:FormikHelpers<IRegisterModel>) => {
        try {
            let image: null|File = null;
            if (values.image && values.image.length > 0) {
                image = values.image[0];
            }
            const model = {...values, image};
            console.log("Register user ", model);

            const result = await axios.post(
                `${APP_ENV.API_BASE_URL}/api/account/register`,
                    model,
                    {
                        headers: {
                            "Content-Type": "multipart/form-data",
                        },
                    }
                );

            const { token } = result.data;
            appDispatch(loginSuccess(token));
            navigate("/");
        } catch (error) {
            console.error("Error:", error);
            //setStatus(error);
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <div className="flex justify-center items-center min-h-screen bg-gray-100 py-10">
            <div className="w-full max-w-md p-8 space-y-6 bg-white rounded-xl shadow-lg">
                <h1 className="text-3xl font-bold text-center text-gray-900">Реєстрація</h1>

                <Formik<IRegisterModel> initialValues={{ firstName: '', lastName: '', email: '', password: '', confirmPassword: '', phone: '', image: null }} validationSchema={RegisterSchema} onSubmit={handleSubmit}>
                    {({ isSubmitting, status, setFieldValue }) => (
                        <Form className="space-y-4">
                            {status && <div className="text-red-500 text-center text-sm bg-red-50 p-2 rounded">{status}</div>}
                            <div className="flex flex-col items-center gap-2 mb-4">
                                <label
                                    htmlFor="imageUpload"
                                    className="cursor-pointer group relative w-24 h-24 rounded-full overflow-hidden flex items-center justify-center"
                                >

                                    <img
                                        src={preview || `${APP_ENV.API_BASE_URL}/images/default.jpg`}
                                        alt="Avatar"
                                        className="w-full h-full object-cover"
                                    />
                                </label>
                                <input
                                    id="imageUpload"
                                    type="file"
                                    accept="image/*"
                                    className="hidden"
                                    onChange={(e) => {
                                        const file = e.currentTarget.files?.[0];
                                        if (file) {
                                            setFieldValue("image", e.currentTarget.files);
                                            setPreview(URL.createObjectURL(file));
                                        }
                                    }}
                                />
                                <ErrorMessage name="image" component="div" className="text-red-500 text-xs mt-1" />
                            </div>
                            <div className="grid grid-cols-2 gap-4">
                                <div>
                                    <label className="block text-sm font-medium text-gray-700">Ім'я</label>
                                    <Field name="firstName" className="mt-1 block w-full p-2 border border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500" />
                                    <ErrorMessage name="firstName" component="div" className="text-red-500 text-xs mt-1" />
                                </div>
                                <div>
                                    <label className="block text-sm font-medium text-gray-700">Прізвище</label>
                                    <Field name="lastName" className="mt-1 block w-full p-2 border border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500" />
                                    <ErrorMessage name="lastName" component="div" className="text-red-500 text-xs mt-1" />
                                </div>
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700">Email</label>
                                <Field name="email" type="email" className="mt-1 block w-full p-2 border border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500" />
                                <ErrorMessage name="email" component="div" className="text-red-500 text-xs mt-1" />
                            </div>
                            <div>
                                <label className="block text-sm font-medium text-gray-700">Phone</label>
                                <Field name="phone" type="text" placeholder="+380xxxxxxxxx" className="mt-1 block w-full p-2 border border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500" />
                                <ErrorMessage name="phone" component="div" className="text-red-500 text-xs mt-1" />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700">Пароль</label>
                                <Field name="password" type="password" className="mt-1 block w-full p-2 border border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500" />
                                <ErrorMessage name="password" component="div" className="text-red-500 text-xs mt-1" />
                            </div>

                            <div>
                                <label className="block text-sm font-medium text-gray-700">Підтвердження паролю</label>
                                <Field name="confirmPassword" type="password" className="mt-1 block w-full p-2 border border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500" />
                                <ErrorMessage name="confirmPassword" component="div" className="text-red-500 text-xs mt-1" />
                            </div>

                            <button
                                type="submit"
                                disabled={isSubmitting}
                                className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-green-600 hover:bg-green-700 focus:outline-none disabled:bg-gray-400"
                            >
                                {isSubmitting ? "Реєстрація..." : "Створити акаунт"}
                            </button>
                        </Form>
                    )}
                </Formik>

                <div className="text-center text-sm text-gray-600">
                    Вже є акаунт? <Link to="/user/login" className="text-blue-600 hover:underline">Увійти</Link>
                </div>
            </div>
        </div>
    );
};

export default RegisterPage;