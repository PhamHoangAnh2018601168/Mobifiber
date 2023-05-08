$(function () {
    window.AgentsAndDevelopmentUnit = {
        init: function () {
            AgentsAndDevelopmentUnit.action();
            AgentsAndDevelopmentUnit.GetDataAgents();
            AgentsAndDevelopmentUnit.GetDataDevelopmentUnit();
        },
        action: function () {

            $('#btnAddAgents').click(function () {
                var Id = $('#AgentsId').val();
                if (Id > 0) {
                    var r = confirm('Bạn có muốn cập nhật lại thông tin đại lý !')
                    if (r == true) {
                        var url = "/Setting/CreateOrUpdateAgents";
                        AgentsAndDevelopmentUnit.AddOrUpdateNewAgents(Id, url);
                    }
                }
                else {
                    var r = confirm('Bạn có muốn lưu thông tin đại lý !')
                    if (r == true) {
                        var url = "/Setting/CreateOrUpdateAgents";
                        AgentsAndDevelopmentUnit.AddOrUpdateNewAgents(Id, url); //0 là thêm mới
                    }

                }
            });
            $('#btnAddDevelopmentUnit').click(function () {
                var Id = $('#DevelopmentUnitId').val();
                if (Id > 0) {
                    var r = confirm('Bạn có muốn cập nhật lại thông tin đơn vị phát triển !')
                    if (r == true) {
                        var url = "/Setting/CreateOrUpdateDevelopmentUnit";
                        AgentsAndDevelopmentUnit.AddOrUpdateNewDevelopmentUnit(Id, url);
                    }
                }
                else {
                    var r = confirm('Bạn có muốn tạo mới đơn vị phát triển !')
                    if (r == true) {
                        var url = "/Setting/CreateOrUpdateDevelopmentUnit";
                        AgentsAndDevelopmentUnit.AddOrUpdateNewDevelopmentUnit(Id, url); //0 là thêm mới
                    }

                }
            });

            $('#btnCloseAgents').click(function () {
                AgentsAndDevelopmentUnit.Clear();
            });
            $('#btnCloseDevelopmentUnit').click(function () {
                AgentsAndDevelopmentUnit.Clear();
            });
            $('#btnOpenModalAgents').click(function () {
                AgentsAndDevelopmentUnit.Clear();
                $('#exampleModalLabelAgents').text("Thêm đại lý");
                $('#btnAddAgents').text('Lưu');
                $('#AgentsId').attr('value', 0);
            });
            $('#btnOpenModalDevelopmentUnit').click(function () {
                AgentsAndDevelopmentUnit.Clear();
                $('#exampleModalLabel').text("Thêm đơn vị phát triển");
                $('#btnAddDevelopmentUnit').text('Lưu');
                $('#DevelopmentUnitId').attr('value', 0);
            });
        },
        Clear: function () {
            $('#AgentsName').val('');
            $('#AgentsCode').val('');
            $('#DescriptionAgents').val('');
            $('#AgentsAddr').val('');
            $('#AgentsTaxCode').val('');
            $('#AgentsPhone').val('');

            $('#DevelopmentUnitName').val('');
            $('#DevelopmentUnitCode').val('');
            $('#DescriptionDevelopmentUnit').val('');

        },

        AddOrUpdateNewAgents: function (Id, url) {
            var AgentsName = $('#AgentsName').val();
            var AgentsCode = $('#AgentsCode').val();
            var AgentsAddr = $('#AgentsAddr').val();
            var AgentsTaxCode = $('#AgentsTaxCode').val();
            var AgentsPhone = $('#AgentsPhone').val();
            var DescriptionAgents = $('#DescriptionAgents').val();

            if (AgentsName == "") {
                toastr.error("Vui lòng nhập tên đại lý !");
                return;
            }
            if (AgentsCode == "") {
                toastr.error("Vui lòng nhập mã đại lý !");
                return;
            }
            
            if (Id == 0) {
                common.buttonLoader('btnAddAgents', 'start', 'Lưu');
            }
            else {
                common.buttonLoader('btnAddAgents', 'start', 'Cập nhật');
            }

            $.ajax({
                type: 'post',
                url: url,
                data: {
                    Id: Id,
                    AgentsName: AgentsName,
                    AgentsCode: AgentsCode,
                    AgentsAddr: AgentsAddr,
                    AgentsTaxCode: AgentsTaxCode,
                    AgentsPhone: AgentsPhone,
                    DescriptionAgents: DescriptionAgents,
                },
                success: function (rp) {
                    if (Id == 0) {
                        common.buttonLoader('btnAddAgents', 'stop', 'Lưu');
                    }
                    else {
                        common.buttonLoader('btnAddAgents', 'stop', 'Cập nhật');
                    }
                    if (rp.status) {
                        toastr.success(rp.mess);
                        AgentsAndDevelopmentUnit.Clear();
                        $('#CreateAgents').modal('hide');
                        $("#tblAgents").bootstrapTable('refresh');
                    } else {
                        toastr.error(rp.mess);
                    }
                },

            });
        },
        GetDataAgents: function () {
            $('#tblAgents').bootstrapTable({
                url: '/Setting/GetDataAgents',
                method: "get",
                queryParams: function (p) {
                    return {
                        search: p.search,
                        offset: p.offset,
                        limit: p.limit
                    };
                },
                pageSize: 10,
                pagination: true,
                paginationVAlign: 'bottom',
                sidePagination: 'server',
                striped: true,
                soft: true,
                search: false,
                showColumns: false,
                showRefresh: false,
                minimumCountColumns: 0,

                columns: [
                    {
                        title: "Thao tác",
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return '<a href="javascript:void(0)" data-id = ' + row.amId + ' class="btnEditAgents mr-1" title="Cập nhật" data-toggle="modal" data-target="#CreateAgents"><i class="fa fa-edit"></i></a><a href="javascript:void(0)" data-id = ' + row.amId + ' class="btnDeleteAgents" title="xóa" ><i class="fa fa-trash-o"></i></a>';
                        },
                        events: {
                            'click .btnEditAgents': function (e, value, row, index) {
                                $('#exampleModalLabelAgents').text("Cập nhật thông tin đại lý");
                                $('#btnAddAgents').text('Cập nhật');
                                $('#AgentsId').attr('value', row.amId);
                                $('#AgentsName').val(row.agentsName);
                                $('#AgentsCode').val(row.agentCode);
                                $('#AgentsAddr').val(row.address);
                                $('#AgentsTaxCode').val(row.taxCode);
                                $('#AgentsPhone').val(row.phoneNumber);
                                $('#DescriptionAgents').val(row.description);
                            },
                            'click .btnDeleteAgents': function (e, value, row, index) {
                                var r = confirm('Bạn có muốn xóa thông tin đại lý này !')
                                if (r == true) {
                                    $.ajax({
                                        type: 'post',
                                        url: '/Setting/DeleteAgents',
                                        data: {
                                            Id: row.amId
                                        },
                                        success: function (rp) {
                                            if (rp.status) {
                                                toastr.success(rp.mess);
                                                $("#tblAgents").bootstrapTable('refresh');
                                            } else {
                                                toastr.error(rp.mess);
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    },
                    {
                        field: "agentsName",
                        title: "Tên đại lý",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "agentCode",
                        title: "Mã đại lý",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "description",
                        title: "Địa chỉ",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "taxCode",
                        title: "Mã số thuế",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "phoneNumber",
                        title: "SĐT",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "description",
                        title: "Mô tả",
                        align: 'left',
                        valign: 'middle',

                    },

                ],
                onLoadSuccess: function (data) {
                    if (data.total > 0 && data.rows.length === 0) {
                        $(this).bootstrapTable('load', data.rows);
                    };
                },

            });
        },

        AddOrUpdateNewDevelopmentUnit: function (Id, url) {
            var DevelopmentUnitName = $('#DevelopmentUnitName').val();
            var DevelopmentUnitCode = $('#DevelopmentUnitCode').val();
            var DevelopmentUnitAddr = $('#DevelopmentUnitAddr').val();
            var DevelopmentUnitTaxCode = $('#DevelopmentUnitTaxCode').val();
            var DevelopmentUnitPhone = $('#DevelopmentUnitPhone').val();
            var DescriptionDevelopmentUnit = $('#DescriptionDevelopmentUnit').val();
            if (DevelopmentUnitName == "") {
                toastr.error("Vui lòng nhập tên đơn vị phát triển !");
                return;
            }
            if (DevelopmentUnitCode == "") {
                toastr.error("Vui lòng nhập mã đơn vị phát triển !");
                return;
            }
            
            if (Id == 0) {
                common.buttonLoader('btnAddDevelopmentUnit', 'start', 'Lưu');
            }
            else {
                common.buttonLoader('btnAddDevelopmentUnit', 'start', 'Cập nhật');
            }

            $.ajax({
                type: 'post',
                url: url,
                data: {
                    Id: Id,
                    DevelopmentUnitName: DevelopmentUnitName,
                    DevelopmentUnitCode: DevelopmentUnitCode,
                    DevelopmentUnitAddr: DevelopmentUnitAddr,
                    DevelopmentUnitTaxCode: DevelopmentUnitTaxCode,
                    DevelopmentUnitPhone: DevelopmentUnitPhone,
                    DescriptionDevelopmentUnit: DescriptionDevelopmentUnit,
                },
                success: function (rp) {
                    if (Id == 0) {
                        common.buttonLoader('btnAddDevelopmentUnit', 'stop', 'Lưu');
                    }
                    else {
                        common.buttonLoader('btnAddDevelopmentUnit', 'stop', 'Cập nhật');
                    }
                    if (rp.status) {
                        toastr.success(rp.mess);
                        AgentsAndDevelopmentUnit.Clear();
                        $('#CreateDevelopmentUnit').modal('hide');
                        $("#tblDevelopmentUnit").bootstrapTable('refresh');
                    } else {
                        toastr.error(rp.mess);
                    }
                },

            });
        },
        GetDataDevelopmentUnit: function () {
            $('#tblDevelopmentUnit').bootstrapTable({
                url: '/Setting/GetDataDevelopmentUnit',
                method: "get",
                queryParams: function (p) {
                    return {
                        search: p.search,
                        offset: p.offset,
                        limit: p.limit
                    };
                },
                pageSize: 10,
                pagination: true,
                paginationVAlign: 'bottom',
                sidePagination: 'server',
                striped: true,
                soft: true,
                search: false,
                showColumns: false,
                showRefresh: false,
                minimumCountColumns: 0,

                columns: [
                    {
                        title: "Thao tác",
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            return '<a href="javascript:void(0)" data-id = ' + row.developId + ' class="btnEditDevelopmentUnit mr-1" title="Cập nhật"  data-toggle="modal" data-target="#CreateDevelopmentUnit"><i class="fa fa-edit"></i></a><a href="javascript:void(0)" data-id = ' + row.developId + ' class="btnDeleteDevelopmentUnit" title="xóa"  ><i class="fa fa-trash-o"></i></a>';
                        },
                        events: {
                            'click .btnEditDevelopmentUnit': function (e, value, row, index) {
                                $('#exampleModalLabel').text("Cập nhật thông tin đơn vị phát triển");
                                $('#btnAddDevelopmentUnit').text('Cập nhật');
                                $('#DevelopmentUnitId').attr('value', row.developId);
                                $('#DevelopmentUnitName').val(row.developName);
                                $('#DevelopmentUnitCode').val(row.developCode);
                                $('#DevelopmentUnitAddr').val(row.address);
                                $('#DevelopmentUnitTaxCode').val(row.taxCode);
                                $('#DevelopmentUnitPhone').val(row.phoneNumber);
                                $('#DescriptionDevelopmentUnit').val(row.description);
                            },
                            'click .btnDeleteDevelopmentUnit': function (e, value, row, index) {
                                var r = confirm('Bạn có muốn xóa thông tin đơn vị phát triển này !')
                                if (r == true) {
                                    $.ajax({
                                        type: 'post',
                                        url: '/Setting/DeleteDevelopmentUnit',
                                        data: {
                                            Id: row.developId
                                        },
                                        success: function (rp) {
                                            if (rp.status) {
                                                toastr.success(rp.mess);
                                                $("#tblDevelopmentUnit").bootstrapTable('refresh');
                                            } else {
                                                toastr.error(rp.mess);
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    },
                    {
                        field: "developName",
                        title: "Tên đơn vị phát triển",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "developCode",
                        title: "Mã đơn vị phát triển",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "description",
                        title: "Địa chỉ",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "taxCode",
                        title: "Mã số thuế",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "phoneNumber",
                        title: "SĐT",
                        align: 'left',
                        valign: 'middle',

                    },
                    {
                        field: "description",
                        title: "Mô tả",
                        align: 'left',
                        valign: 'middle',

                    },

                ],
                onLoadSuccess: function (data) {
                    if (data.total > 0 && data.rows.length === 0) {
                        $(this).bootstrapTable('load', data.rows);
                    };
                },

            });
        },
    }
});
$(document).ready(function () {
    AgentsAndDevelopmentUnit.init();
});