import CreateProduct from '../components/create-product'
import HomePage from 'components/home'

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
  }
]
