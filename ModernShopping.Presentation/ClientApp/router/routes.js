import CreateProduct from '../components/create-product'
import HomePage from 'components/home-page'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home' },
  { name: 'create-product', path: '/create-product', component: CreateProduct, display: 'Create product' }
]
