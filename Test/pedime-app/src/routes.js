import Balances from './components/balances/Balances'
import Clientes from './components/clientes/Clientes'
import Empresas from './components/empresas/Empresas'
import Login from './components/login/Login'
import Logout from './components/login/Logout'
import Pedidos from './components/pedidos/Pedidos'
import Productos from './components/productos/Productos'
import Promociones from './components/promociones/Promociones'
import Register from './components/register/Register'
import Sucursales from './components/sucursales/Sucursales'
import Usuarios from './components/usuarios/Usuarios'



const routes = [
    {
        path : '',
        name: 'home',
        components:{
            default: Pedidos    
        },
        meta: {
            requiresVisitor: true
        }  
    },

    {
        path: '/pedidos', 
        name:'pedidos',
        component: Pedidos,        
        meta: {
            requiresVisitor: true
        }  
    },
    {
        path: '/productos', 
        name:'productos',
        component: Productos,        
        meta: {
            requiresVisitor: true
        }  
    },

    {
        path: '/balances', 
        name:'balances',
        component: Balances,        
        meta: {
            requiresVisitor: true
        }  
    },

    {
        path: '/clientes', 
        name:'clientes',
        component: Clientes,        
        meta: {
            requiresVisitor: true
        }  
    },

    {
        path: '/empresas', 
        name:'empresas',
        component: Empresas,        
        meta: {
            requiresVisitor: true
        }  
    },

    {
        path: '/promociones', 
        name:'promociones',
        component: Promociones,        
        meta: {
            requiresVisitor: true
        }  
    },

    {
        path: '/sucursales', 
        name:'sucursales',
        component: Sucursales,        
        meta: {
            requiresVisitor: true
        }  
    },

    {
        path: '/usuarios', 
        name:'usuarios',
        component: Usuarios,        
        meta: {
            requiresVisitor: true
        }  
    },
    
    {
        path: '/register', 
        name:'register',
        component: Register,
        meta: {
            requiresVisitor: false
        }
    },
    {
        path: '/login', 
        name:'login',
        component: Login,
        meta: {
            requiresVisitor: false
        }
    },
    {
        path: '/logout', 
        name:'logout',
        component: Logout
    },
    

]

export default routes