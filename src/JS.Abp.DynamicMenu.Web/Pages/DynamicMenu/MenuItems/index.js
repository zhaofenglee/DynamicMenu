$(function () {
    var l = abp.localization.getResource("DynamicMenu");
	
	var menuItemService = window.jS.abp.dynamicMenu.controllers.menuItems.menuItem;
    
    var selectedNodeId = null;
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "DynamicMenu/MenuItems/CreateModal",
        scriptUrl: abp.appPath + "Pages/DynamicMenu/MenuItems/createModal.js",
        modalClass: "menuItemCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "DynamicMenu/MenuItems/EditModal",
        scriptUrl: abp.appPath + "Pages/DynamicMenu/MenuItems/editModal.js",
        modalClass: "menuItemEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            name: $("#NameFilter").val(),
			displayName: $("#DisplayNameFilter").val(),
            isActive: (function () {
                var value = $("#IsActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			url: $("#UrlFilter").val(),
			icon: $("#IconFilter").val(),
			orderMin: $("#OrderFilterMin").val(),
			orderMax: $("#OrderFilterMax").val(),
			target: $("#TargetFilter").val(),
			elementId: $("#ElementIdFilter").val(),
			cssClass: $("#CssClassFilter").val(),
			permission: $("#PermissionFilter").val(),
			resourceTypeName: $("#ResourceTypeNameFilter").val(),
			parentId: $("#ParentIdFilter").val()
        };
    };

    $('#DirectoryTree')
        .on('select_node.jstree', function (e, data) {
            var node = data.node;
            console.log(node);
            $(".selected-cache-name").html(node.text);
            selectedNodeId = node.id === 'j1_1' ? null : node.id;
            $('#MenuItemsTable').DataTable().ajax.url(abp.libs.datatables.createAjax(menuItemService.getPageLookup, function () {
                return {
                    filterText: $("#FilterText").val(),
                    parentId: node.id==='j1_1'?null:node.id,
                };
            })).load();
        })
        .on('move_node.jstree', function (e, data) {
            $('#DirectoryTree').jstree("refresh");
        })
        .jstree({
            'core': {
                'data': function (obj, callback) {
                    menuItemService.getTree().done(function (trees) {
                        var mapToJsTreeNode = function (values) {
                            var map = function (value) {
                                return {
                                    id: value.id,
                                    text: value.displayName,
                                    children: mapToJsTreeNode(value.children),
                                    state: {
                                        opened: false
                                    },
                                };
                            }

                            if (values) {
                                if (_.isArray(values)) {
                                    return _.map(values, function (value) {
                                        return map(value);
                                    });
                                } else {
                                    return map(values);
                                }
                            }
                        };

                        if (_.isArray(trees)) {
                            callback.call(this, mapToJsTreeNode(trees));
                        } else {

                        }
                    });

                },
                "check_callback": true
            },
            "plugins": ["dnd"]
        });
    
    
    
    var dataTableColumns = [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('DynamicMenu.MenuItems.Edit'),
                                action: function (data) {
                                    editModal.open({
                                        id: data.record.id
                                    });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('DynamicMenu.MenuItems.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    menuItemService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "name" },
			{ data: "displayName" },
            {
                data: "isActive",
                render: function (isActive) {
                    return isActive ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
			{ data: "url" },
			{ data: "icon" },
			{ data: "order" },
			{ data: "target" },
			{ data: "elementId" },
			{ data: "cssClass" },
			{ data: "permission" },
			{ data: "resourceTypeName" }
			        
    ];
    
    

    var dataTable = $("#MenuItemsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(menuItemService.getPageLookup, getFilter),
        columnDefs: dataTableColumns
    }));
    
    

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
    });

    $("#NewMenuItemButton").click(function (e) {
        e.preventDefault();
        console.log(selectedNodeId);
        createModal.open({ parentId: selectedNodeId });
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        menuItemService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/dynamic-menu/menu-items/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'name', value: input.name }, 
                            { name: 'displayName', value: input.displayName }, 
                            { name: 'isActive', value: input.isActive }, 
                            { name: 'url', value: input.url }, 
                            { name: 'icon', value: input.icon },
                            { name: 'orderMin', value: input.orderMin },
                            { name: 'orderMax', value: input.orderMax }, 
                            { name: 'target', value: input.target }, 
                            { name: 'elementId', value: input.elementId }, 
                            { name: 'cssClass', value: input.cssClass }, 
                            { name: 'permission', value: input.permission }, 
                            { name: 'resourceTypeName', value: input.resourceTypeName }, 
                            { name: 'parentId', value: input.parentId }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reloadEx();;
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reloadEx();;
    });
    
    
    
    
    
    
    
    
});
