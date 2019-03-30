import Oidc from 'oidc-client'

const manager = new Oidc.UserManager({
  authority: 'https://localhost:5000',
  client_id: 'VueJs',
  redirect_uri: 'https://localhost:44391/callback',
  response_type: 'id_token token',
  scope: 'openid profile api1',
  post_logout_redirect_uri: 'https://localhost:44391/',
  userStore: new Oidc.WebStorageStateStore({
    store: window.localStorage
  })
})
console.log(manager)

export default manager
