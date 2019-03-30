<template>
  <div id="app" class="container bg-light">
    <notifications group="general-message"/>
    <nav-menu/>
    <div class="row app-body pt-4 pb-4">
      <div class="col">
        <transition enter-active-class="animated slideInUp" mode="out-in">
          <router-view></router-view>
        </transition>
      </div>
    </div>
  </div>
</template>

<script>
import NavMenu from './nav-menu'
import manager from '../security/security'

export default {
  components: {
    'nav-menu': NavMenu
  },
  data: {
    isAuthenticate: false,
    user: null,
    manager: manager
  },
  methods: {
    async authenticate(returnPath) {
      // not implement yet
      const user = await this.$root.getUser()

      if (user) {
        this.isAuthenticate = true
        this.user = user
      } else {
        await this.$root.signIn(returnPath)
      }
    },
    async getUser() {
      try {
        const user = await this.manager.getUser()
        return user
      } catch (error) {
        console.log(error)
      }
    },
    async signIn(returnPath) {
      returnPath ? this.manager.signinRedirect({
        state: returnPath
      }) : this.manager.signinRedirect()
    }
  },
}
</script>
