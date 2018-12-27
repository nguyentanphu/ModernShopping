<template>
    <div id="app" class="container bg-light">
        <nav-menu/>
        <div class="row app-body">
            <div class="col">
                <transition enter-active-class="animated slideInUp" mode="out-in">
                    <router-view></router-view>
                </transition>
            </div>
        </div>
    </div>
</template>

<script>
import NavMenu from "./nav-menu";

export default {
    components: {
        "nav-menu": NavMenu
    },

    data() {
        return {};
    }
};
</script>

<style scope>
.app-body {
    margin-top: 30px;
}
</style>
