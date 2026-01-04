import Header from "./components/Header";
import {BrowserRouter, Outlet, Route, Routes} from "react-router-dom";
import {useAppSelector} from "./store";
import HomePage from "./pages/Home";
import AboutPage from "./pages/About";
import LogIn from "./pages/Users/LogIn.tsx";
import AppLayout from "./admin/layout/AppLayout.tsx";
import UserProfiles from "./admin/pages/UserProfiles.tsx";
import HomeAdmin from "./admin/pages/Dashboard/Home.tsx";
import Calendar from "./admin/pages/Calendar.tsx";
import Blank from "./admin/pages/Blank.tsx";
import FormElements from "./admin/pages/Forms/FormElements.tsx";
import BasicTables from "./admin/pages/Tables/BasicTables.tsx";
import Alerts from "./admin/pages/UiElements/Alerts.tsx";
import Avatars from "./admin/pages/UiElements/Avatars.tsx";
import Badges from "./admin/pages/UiElements/Badges.tsx";
import Buttons from "./admin/pages/UiElements/Buttons.tsx";
import Images from "./admin/pages/UiElements/Images.tsx";
import Videos from "./admin/pages/UiElements/Videos.tsx";
import LineChart from "./admin/pages/Charts/LineChart.tsx";
import BarChart from "./admin/pages/Charts/BarChart.tsx";
import SignIn from "./admin/pages/AuthPages/SignIn.tsx";
import SignUp from "./admin/pages/AuthPages/SignUp.tsx";
import NotFound from "./admin/pages/OtherPage/NotFound.tsx";
import RegisterPage from "./pages/Users/Register.tsx";


const MainLayout = () => {
    return (
        <div className="min-h-screen bg-gray-100">
            <Header />

            <main className="p-6">
                <Outlet />
            </main>
        </div>
    );
};

export default function App() {
    const user =
        useAppSelector(redux => redux.auth.user);

    console.log("User roles", user?.roles);
    return (
        <BrowserRouter>
            <Routes>
                <Route path={"/"} element={<MainLayout/>}>
                    <Route index element={<HomePage/>}/>
                    <Route path="about" element={<AboutPage /> }/>,
                    <Route path="user/LogIn" element={<LogIn /> }/>
                    <Route path="user/register" element={<RegisterPage /> }/>
                </Route>
                {/* Dashboard Layout */}
                <Route path={"/admin"} element={<AppLayout />}>
                    <Route index element={<HomeAdmin/>} />

                    {/* Others Page */}
                    <Route path="profile" element={<UserProfiles />} />
                    <Route path="calendar" element={<Calendar />} />
                    <Route path="blank" element={<Blank />} />

                    {/* Forms */}
                    <Route path="form-elements" element={<FormElements />} />

                    {/* Tables */}
                    <Route path="basic-tables" element={<BasicTables />} />

                    {/* Ui Elements */}
                    <Route path="alerts" element={<Alerts />} />
                    <Route path="avatars" element={<Avatars />} />
                    <Route path="badge" element={<Badges />} />
                    <Route path="buttons" element={<Buttons />} />
                    <Route path="images" element={<Images />} />
                    <Route path="videos" element={<Videos />} />

                    {/* Charts */}
                    <Route path="line-chart" element={<LineChart />} />
                    <Route path="bar-chart" element={<BarChart />} />
                </Route>
                {/* Auth Layout */}
                <Route path="/signin" element={<SignIn />} />
                <Route path="/signup" element={<SignUp />} />

                {/* Fallback Route */}
                <Route path="*" element={<NotFound />} />
            </Routes>
        </BrowserRouter>

    );
}
