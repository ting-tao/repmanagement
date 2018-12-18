<template>


    <div>
        <p>代办事项</p>
        <p>
            <span>您有 《{{num1}}》份简历待评价,请及时处理!</span>
            <span>您有 《{{num2}}》份简历最终面试未通过,请查看!</span>
        </p>
    </div>

</template>


<script>

    export default {
        data() {
            return {
                num1: 33,
                num2: 2,
                
            }
        },
        methods: {
        },
        computed:{
            num3:function(){
                return this.$route.meta;
            }
        }
    }
</script>

<style>

</style>