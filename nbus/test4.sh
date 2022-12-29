muxbuspoke8x2() {
        local x

        let x=$1
        x=$((x >> 11))
        tshwctl -a 0x70 -w $x
        let x=$1
        let x="(x & 0x7ff) | 0x7000"
        tshwctl -a 0x72 -w $x
        tshwctl -a 0x76 -w $2
}


muxbuspeek8x2() {
        local x

        let x=$1
        x=$((x >> 11))
        tshwctl -a 0x70 -w $x
        let x=$1
        let x="(x & 0x7ff) | 0x7000"
        tshwctl -a 0x72 -w $x
        tshwctl -a 0x76 -r
}

muxbuspoke8x2 "0x300" "0x1000"