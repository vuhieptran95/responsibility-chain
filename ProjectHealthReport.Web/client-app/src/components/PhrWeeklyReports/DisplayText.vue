<template>
    <p :class="'mt-4 '+ classs">{{text}}</p>
</template>

<script lang="ts">
    import {Vue, Component, Prop} from 'vue-property-decorator'

    @Component
    export default class DisplayText extends Vue {
        @Prop() text!: string;
        @Prop() classs!: string;
    }

</script>