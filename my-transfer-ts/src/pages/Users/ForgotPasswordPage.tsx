import { useState } from 'react';
import {Formik, Form, Field, ErrorMessage, type FormikHelpers} from 'formik';
import * as yup from 'yup';
import { Link } from 'react-router-dom';
import { Loader2, Mail } from 'lucide-react';
import type {IForgotPassword} from "../../Interfaces/User/IForgotPassword.ts";
import axios from "axios";
import APP_ENV from "../../env";

const validationSchema = yup.object({
    email: yup.string().email("Некоректна пошта").required("Введіть пошту"),
});

const ForgotPasswordPage = () => {
    const [isSent, setIsSent] = useState(false);
    const [serverError, setServerError] = useState<string | null>(null);

    const handleSubmit = async (values: IForgotPassword, { setSubmitting }: FormikHelpers<IForgotPassword>) => {
        try {
            setServerError(null);
            await axios.post(`${APP_ENV.API_BASE_URL}/api/account/forgotPassword`, values);
            console.log("Forgot password request", values);
            //await forgotPasswordRequest(values);
            setIsSent(true);
        } catch (error: any) {
            setServerError(error.response?.data?.Errors?.Email || "Щось пішло не так");
        } finally {
            setSubmitting(false);
        }
    };

    if (isSent) {
        return (
            <div className="flex justify-center items-center min-h-screen bg-gray-100 p-4">
                <div className="max-w-md w-full bg-white rounded-xl shadow-lg p-8 text-center">
                    <div className="mx-auto w-12 h-12 bg-green-100 rounded-full flex items-center justify-center mb-4">
                        <Mail className="text-green-600" size={24} />
                    </div>
                    <h2 className="text-2xl font-bold text-gray-900 mb-2">Лист відправлено</h2>
                    <p className="text-gray-600 mb-6">
                        Ми надіслали інструкції для відновлення пароля на вашу пошту.
                    </p>
                    <Link to="/login" className="text-blue-600 hover:underline font-medium">
                        Повернутися до входу
                    </Link>
                </div>
            </div>
        );
    }

    return (
        <div className="flex justify-center items-center min-h-screen bg-gray-100 p-4">
            <div className="max-w-md w-full bg-white rounded-xl shadow-lg p-8">
                <h2 className="text-2xl font-bold text-center text-gray-900 mb-2">Забули пароль?</h2>
                <p className="text-center text-gray-500 mb-6 text-sm">
                    Введіть вашу електронну пошту, і ми надішлемо вам посилання для зміни пароля.
                </p>

                {serverError && (
                    <div className="bg-red-50 text-red-600 p-3 rounded-md text-sm mb-4 text-center">
                        {serverError}
                    </div>
                )}

                <Formik
                    initialValues={{ email: '' }}
                    validationSchema={validationSchema}
                    onSubmit={handleSubmit}
                >
                    {({ isSubmitting }) => (
                        <Form className="space-y-4">
                            <div>
                                <label htmlFor="email" className="block text-sm font-medium text-gray-700 mb-1">Email</label>
                                <Field
                                    name="email"
                                    type="email"
                                    placeholder="xxxx@gmail.com"
                                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-all"
                                />
                                <ErrorMessage name="email" component="div" className="text-red-500 text-xs mt-1" />
                            </div>

                            <button
                                type="submit"
                                disabled={isSubmitting}
                                className="w-full bg-blue-600 hover:bg-blue-700 text-white font-medium py-2.5 rounded-lg transition-colors flex justify-center items-center disabled:bg-blue-400"
                            >
                                {isSubmitting ? <Loader2 className="animate-spin mr-2" size={20} /> : "Відправити"}
                            </button>
                        </Form>
                    )}
                </Formik>

                <div className="mt-6 text-center text-sm">
                    <Link to="/login" className="text-gray-600 hover:text-blue-600 transition-colors">
                        ← Назад
                    </Link>
                </div>
            </div>
        </div>
    );
};

export default ForgotPasswordPage;