import CreateProduct from '../components/create-product'
import HomePage from '../components/home'
import Callback from '../components/callback'

export const routes = [{
    name: 'home',
    path: '/',
    component: HomePage,
    display: 'Home',
    meta: {
      requireAuth: false
    }
  },
  {
    name: 'create-product',
    path: '/create-product',
    component: CreateProduct,
    display: 'Create product',
    meta: {
      requireAuth: true
    }
  },
  {
    name: 'callback',
    path: '/callback',
    component: Callback
  }
]
