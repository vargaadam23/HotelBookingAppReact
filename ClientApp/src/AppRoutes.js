import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Reserve } from './pages/Reserve';
import { Home } from "./pages/Home";
import { Rooms } from "./pages/Rooms";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/counter',
        element: <Counter />
    },
    {
        path: '/fetch-data',
        requireAuth: true,
        element: <FetchData />
    },
    {
        path: '/rooms',
        element: <Rooms />
    },
    {
        path: '/reserve',
        element: <Reserve />
    },
    ...ApiAuthorzationRoutes
];

export default AppRoutes;
